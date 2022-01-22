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
        private readonly ShopDbContext _context;
        private readonly int userId = 1;

        public TransactionController(ShopDbContext context)
        {
            _context = context;
        }

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

        private ShippingType GetShippingType(string key)
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
                case "slefPickup":
                    ret = ShippingType.SelfPickup;
                    break;
                default:
                    ret = ShippingType.Delivery;
                    break;
            };
            return ret;
        }

        private void DeleteUnusedCookies(List<int> boughtCookies)
        {
            foreach(var c in boughtCookies)
            {
                Response.Cookies.Delete(c.ToString());
            }
        }
    }
}
