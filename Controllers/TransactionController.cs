using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PO_Projekt.Data;
using PO_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PO_Projekt.Controllers
{
    public class TransactionController : Controller
    {
        /// <summary>
        /// A database context that allows for the access to the local database.
        /// </summary>
        /// <value>ShopDbContext from root/Data</value>
        private readonly ShopDbContext _context;
        /// <summary>
        /// Temporary field. In the future this parameter will be removed. It represents the logged in user.
        /// </summary>
        /// <value>The representation od logged user's id.</value>
        private readonly int userId = 1;

        /// <summary>
        /// This is a constructor for TransactionController
        /// </summary>
        /// <param name="context">This parameter is a database context that allows the controller to access database objects</param>
        public TransactionController(ShopDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// An async method that passes transaction related data of a logged in user to the Transaction Details View.
        /// </summary>
        /// <returns>Returns the Transaction Details View with user shipping data.</returns>
        public async Task<IActionResult> Details()
        {
            var userData = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            var prescriptionData = await _context.Prescriptions
                .Where(p => p.UserId == userId)
                .Where(p => p.EndDate >= DateTime.Now).ToListAsync();

            var shippingData = await _context.ShippingData
                .Where(sd => sd.UserId == userId)
                .ToListAsync();

            userData.AllShippingData = shippingData;
            userData.AllPrescriptions = prescriptionData;

            var allCartIds = Request.Cookies.Select(item => item.Key).ToList();

            var selectedArticles = await _context.ProductNames
                .Where(pn => allCartIds.Contains(pn.Id.ToString()))
                .ToListAsync();

            userData.SelectedProducts = selectedArticles;

            double totalPrice = 0;
            foreach(var prod in selectedArticles)
            {
                totalPrice += prod.Price * Double.Parse(Request.Cookies[prod.Id.ToString()]);
            }

            ViewData["Summary"] = totalPrice;

            return View(userData);
        }

        /// <summary>
        /// An async method that is called at the beggining of each transaction.
        /// It directs data to other support methods based on the data binded in Transaction Details View.
        /// The data are saved in the local database if no other treatment is required.
        /// </summary>
        /// <param name="userData">This is an object that contains specified by the the user shipping data for the transaction.</param>
        /// <returns>Returns the Transaction Finish View</returns>
        public async Task<IActionResult> BeginTransaction([Bind("DeliveryOption,PaymentMethod,Name,Email,LastName,Phone,StreetName,HouseNumber,LocalNumber,PostalCode,City")] User userData)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == userData.Email.ToLower());

            if(existingUser != null)
            {
                return await TransactionWithExistingUser(userData);
            }


            var shippingData = new ShippingData()
            {
                City = userData.City,
                Street = userData.StreetName,
                HomeNumber = userData.HouseNumber,
                LocalNumber = userData.LocalNumber,
                PostalNumber = userData.PostalCode,
            };

            var order = new Order()
            {
                ShippingData = shippingData,
                Name = userData.Name,
                LastName = userData.LastName,
                Phone = userData.Phone,
                OrderDate = DateTime.Now,
                ShippingType = GetShippingType(userData.DeliveryOption),
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var storedProducts = Request.Cookies.Select(item => item.Key).ToList();
            var boughtArticles = await _context.ProductNames
                .Where(pn => storedProducts.Contains(pn.Id.ToString()))
                .ToListAsync();

            foreach (var art in boughtArticles)
            {
                var prodOrd = new ProductOrder()
                {
                    ProductId = art.Id,
                    Order = order,
                    Count = Int32.Parse(Request.Cookies[art.Id.ToString()]),
                };
                _context.ProductOrders.Add(prodOrd);
            }

            await _context.SaveChangesAsync();

            DeleteUnusedCookies(boughtArticles.Select(a => a.Id).ToList());
            return View("Finish");
        }

        /// <summary>
        /// An async method that extends the BeginTransaction method. It is called when a logged in user makes transaction.
        /// Under that condition it checkes whether passed by the user data are already in the local database or is there
        /// a requirement to create a new ShippingData object in order to store the new data in the local database.
        /// </summary>
        /// <param name="userData">Data passed by the user.</param>
        /// <returns>Returns the Transaction Finish View</returns>
        private async Task<IActionResult> TransactionWithExistingUser(User userData)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == userData.Email.ToLower());
            var userSavedShipingData = await _context.ShippingData.Where(sd => sd.UserId == user.Id).ToListAsync();
            bool usedOldData = false;
            var usedShippingData = new ShippingData();
            foreach(var data in userSavedShipingData)
            {
                if(data.City.ToLower() == userData.City.ToLower())
                {
                    if(data.PostalNumber == userData.PostalCode)
                    {
                        if(data.HomeNumber.ToLower() == userData.HouseNumber.ToLower())
                        {
                            usedOldData = true;
                            usedShippingData = data;
                        }
                    }
                }
            }
            if (usedOldData)
            {
                return await TransactionWithEistingUserAndOldData(user, usedShippingData, userData);
            }
            return await TransactionWithEistingUserAndNewData(user, userData);
        }

        /// <summary>
        /// An async method that extends the TransactionWithExistinguser method. It saves the transaction data using
        /// the existing user shipping data.
        /// </summary>
        /// <param name="user">Existing user.</param>
        /// <param name="shippingData">Shipping data of the existing user.</param>
        /// <param name="userData">Additional data (such as name or phone number) that are not normally
        /// stored in the  database</param>
        /// <returns>Returns the Transaction Finish View</returns>
        private async Task<IActionResult> TransactionWithEistingUserAndOldData(User user, ShippingData shippingData, User userData)
        {
            var order = new Order()
            {
                ShippingData = shippingData,
                UserId = user.Id,
                Name = userData.Name,
                LastName = userData.LastName,
                Phone = userData.Phone,
                OrderDate = DateTime.Now,
                ShippingType = GetShippingType(userData.DeliveryOption),
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var storedProducts = Request.Cookies.Select(item => item.Key).ToList();
            var boughtArticles = await _context.ProductNames
                .Where(pn => storedProducts.Contains(pn.Id.ToString()))
                .ToListAsync();

            foreach(var art in boughtArticles)
            {
                var prodOrd = new ProductOrder()
                {
                    ProductId = art.Id,
                    Order = order,
                    Count = Int32.Parse(Request.Cookies[art.Id.ToString()]),
                };
                _context.ProductOrders.Add(prodOrd);
            }

            await _context.SaveChangesAsync();

            DeleteUnusedCookies(boughtArticles.Select(a => a.Id).ToList());
            return View("Finish");
        }

        /// <summary>
        /// An async method that extends the TransactionWithExistinguser method. It creates a new ShippingData
        /// object to be stored in the local database.
        /// </summary>
        /// <param name="user">An existing user</param>
        /// <param name="userData">New user shipping data</param>
        /// <returns>Returns the Transaction Finish View</returns>
        private async Task<IActionResult> TransactionWithEistingUserAndNewData(User user, User userData)
        {
            var shippingData = new ShippingData()
            {
                UserId = user.Id,
                City = userData.City,
                Street = userData.StreetName,
                HomeNumber = userData.HouseNumber,
                LocalNumber = userData.LocalNumber,
                PostalNumber = userData.PostalCode,
            };

            var order = new Order()
            {
                ShippingData = shippingData,
                UserId = user.Id,
                Name = userData.Name,
                LastName = userData.LastName,
                Phone = userData.Phone,
                OrderDate = DateTime.Now,
                ShippingType = GetShippingType(userData.DeliveryOption),
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var storedProducts = Request.Cookies.Select(item => item.Key).ToList();
            var boughtArticles = await _context.ProductNames
                .Where(pn => storedProducts.Contains(pn.Id.ToString()))
                .ToListAsync();

            foreach (var art in boughtArticles)
            {
                var prodOrd = new ProductOrder()
                {
                    ProductId = art.Id,
                    Order = order,
                    Count = Int32.Parse(Request.Cookies[art.Id.ToString()]),
                };
                _context.ProductOrders.Add(prodOrd);
            }

            await _context.SaveChangesAsync();
            DeleteUnusedCookies(boughtArticles.Select(a => a.Id).ToList());
            return View("Finish");
        }

        /// <summary>
        /// A private method that returns the ShippingData type by the given key.
        /// </summary>
        /// <param name="key">The key of shipping type</param>
        /// <returns>Enum value that represents the shipping type.</returns>
        public ShippingType GetShippingType(string key)
        {
            ShippingType ret;
            switch (key)
            {
                case "delivery":
                    ret = ShippingType.Delivery;
                    break;
                case "parcelLocker":
                    ret = ShippingType.ParcelLocker;
                    break;
                case "localStore":
                    ret = ShippingType.LocalStore;
                    break;
                case "postOffice":
                    ret = ShippingType.PostOffice;
                    break;
                case "selfPickup":
                    ret = ShippingType.SelfPickup;
                    break;
                default:
                    ret = ShippingType.Delivery;
                    break;
            };
            return ret;
        }

        /// <summary>
        /// A private method that removes all the product cookies after transaction was successful.
        /// </summary>
        /// <param name="boughtCookies">A list of indices of cookies to be removed.</param>
        private void DeleteUnusedCookies(List<int> boughtCookies)
        {
            foreach(var c in boughtCookies)
            {
                Response.Cookies.Delete(c.ToString());
            }
        }
    }
}
