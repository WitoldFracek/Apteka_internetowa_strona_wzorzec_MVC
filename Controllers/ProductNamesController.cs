using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PO_Projekt.Data;
using PO_Projekt.Models;

namespace PO_Projekt.Controllers
{
    public class ProductNamesController : Controller
    {
        private readonly ShopDbContext _context;

        /// <summary>
        /// Controller do wyświetlania produktów sklepowych, możliwe różne filtry.
        /// </summary>
        /// <param name="context"></param>
        public ProductNamesController(ShopDbContext context)
        {
            _context = context;
        }

        public IQueryable<ProductName> getAvaliableList(string? AveilableValue)
        {
            IQueryable<ProductName> shopContextFiltered = null;
            if (AveilableValue == "true" || AveilableValue == null)
            {
                shopContextFiltered = _context.Products.Include(p => p.ProductName).Select(item => item.ProductName).Distinct();
            }
            else
            {
                shopContextFiltered = _context.ProductNames.Select(a => a);
            }
            return shopContextFiltered;
        }

        public static IQueryable<ProductName> getFilteredList(IQueryable<ProductName> shopContextFiltered, int? ProductTypeId, 
            int? ProductFormId, int? ManufacturerId, string? SearchContent, 
            string? PrescriptionValue)
        {
            if (ProductTypeId != null)
            {
                shopContextFiltered = shopContextFiltered.Where<ProductName>(item => item.ProductTypeId == ProductTypeId);
            }
            if (ProductFormId != null)
            {
                shopContextFiltered = shopContextFiltered.Where<ProductName>(item => item.ProductFormId == ProductFormId);
            }
            if (ManufacturerId != null)
            {
                shopContextFiltered = shopContextFiltered.Where<ProductName>(item => item.ManufacturerId == ManufacturerId);
            }
            if (SearchContent != null && SearchContent != "")
            {
                SearchContent = SearchContent.Replace('+', ' ');
                shopContextFiltered = shopContextFiltered.Where<ProductName>(item => item.Name.ToLower().Contains(SearchContent.ToLower()));
            }
            if (PrescriptionValue == "true")
            {
                shopContextFiltered = shopContextFiltered.Where<ProductName>(item => item.RequiresPrescription == true);
            }
            else
            {
                shopContextFiltered = shopContextFiltered.Where<ProductName>(item => item.RequiresPrescription == false);
            }
            return shopContextFiltered;
        }

        public static IQueryable<ProductName> getSortedList(IQueryable<ProductName> shopContextFiltered, int? SorterId)
        {
            if (SorterId == 0)
            {
                shopContextFiltered = shopContextFiltered.OrderByDescending(item => item.Price);
            }
            if (SorterId == 1)
            {
                shopContextFiltered = shopContextFiltered.OrderBy(item => item.Price);
            }
            return shopContextFiltered;
        }

        // GET: ProductNames
        /// <summary>
        /// Wyświetla nazwy produktów dla danych filtrów.
        /// </summary>
        /// <param name="ProductTypeId">Filtr na typ produktu (np. leki)</param>
        /// <param name="ProductFormId">Filtr na formę produkut (np. tabletka)</param>
        /// <param name="ManufacturerId">Filtr na producenta</param>
        /// <param name="SearchContent">Zawartość treści wyszukiwania</param>
        /// <param name="PrescriptionValue">Czy na receptę, czy nie.</param>
        /// <param name="AveilableValue">Czy jest obecnie dostępny w sklepie.</param>
        /// <param name="SorterId">Id na metodę sortowania</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(int? ProductTypeId, int? ProductFormId, int? ManufacturerId, string? SearchContent, 
            string? PrescriptionValue, string? AveilableValue, int? SorterId)
        {
            var allCartIds = Request.Cookies.Select(item => item.Key).ToList();
            var allCartArticles = _context.ProductNames
                .Where<ProductName>(item => allCartIds.Contains(item.Id.ToString()))
                ;
            int count = 0;
            foreach (var article in allCartArticles)
            {
                count += Int32.Parse(Request.Cookies[article.Id.ToString()]);
                article.ShoppingCartCount = Int32.Parse(Request.Cookies[article.Id.ToString()]);
            }
            if(count > 0)
            {
                ViewData["CartCount"] = count;
            }            

            var shopDbContext = _context.ProductNames.Include(p => p.Manufacturer).Include(p => p.ProductForm).Include(p => p.ProductType);
            ViewData["ProductTypeList"] = new SelectList(_context.ProductTypes, "Id", "Name", ProductTypeId);
            ViewData["ProductFormList"] = new SelectList(_context.ProductForms, "Id", "Name", ProductFormId);
            ViewData["ManufacturerList"] = new SelectList(_context.Manufacturers, "Id", "Name", ManufacturerId);
            ViewData["SortersList"] = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Selected = false, Text = "Price down", Value = "0" },
                    new SelectListItem { Selected = false, Text = "Price up", Value = "1" },
                }, "Value", "Text", SorterId);

            IQueryable<ProductName> shopContextFiltered = getAvaliableList(AveilableValue);
            shopContextFiltered = getFilteredList(shopContextFiltered, ProductTypeId, ProductFormId, ManufacturerId, SearchContent, PrescriptionValue);
            shopContextFiltered = getSortedList(shopContextFiltered, SorterId);

            if (AveilableValue == "true" || AveilableValue == null)
            {
                ViewData["Aveilable"] = "true";
            }
            else
            {
                ViewData["Aveilable"] = "false";
            }

            if (SearchContent != null && SearchContent != "")
            {
                SearchContent = SearchContent.Replace('+', ' ');
                ViewData["SearchContent"] = SearchContent;
            }
            if (PrescriptionValue == "true")
            {
                ViewData["Prescription"] = "true";
            }
            else
            {
                ViewData["Prescription"] = "false";
            }

            foreach (var article in shopContextFiltered)
            {
                article.AvailableAmount = 0;
            }
            foreach (var article in allCartArticles)
            {
                article.AvailableAmount = 0;
            }

            var products = _context.Products;
            var availableSum = 0;
            foreach (var product in products)
            {
                var firstItem = allCartArticles
                    .Where<ProductName>(item => item.Id == product.ProductNameId)
                    .FirstOrDefault<ProductName>();
                if (firstItem != null)
                {
                    firstItem.AvailableAmount += 1;
                    availableSum += 1;
                }
                firstItem = shopContextFiltered
                    .Where<ProductName>(item => item.Id == product.ProductNameId)
                    .FirstOrDefault<ProductName>();
                if (firstItem != null)
                {
                    firstItem.AvailableAmount += 1;
                }
            }
            foreach (var pn in allCartArticles)
            {
                if (pn.AvailableAmount < pn.ShoppingCartCount)
                {
                    ViewData["CartOk"] = false;
                    ViewData["CartChangeMessage"] = "Some of the products added to shopping cart are unavailable.";
                }
            }

            return View(await shopContextFiltered.ToListAsync());
        }

        // GET: ProductNames/Details/5
        /// <summary>
        /// detale dla konkretnej nazwy produktu
        /// </summary>
        /// <param name="id">id nazwy produktu dla której wyświetlane są detale</param>
        /// <param name="diff">dodatkowy parametr, przy dodawaniu/odejmowaniu produktu z koszyka wartość -1 lub +1</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id, int? diff)
        {
            var allCartIds = Request.Cookies.Select(item => item.Key).ToList();
            var allCartArticles = _context.ProductNames
                .Where<ProductName>(item => allCartIds.Contains(item.Id.ToString()))
                ;
            int count = 0;
            foreach (var article in allCartArticles)
            {
                count += Int32.Parse(Request.Cookies[article.Id.ToString()]);
            }
            if (diff != null)
            {
                count += (int)diff;
            }
            if (count < 0)
            {
                count = 0;
            }
            if (count > 0)
            {
                ViewData["CartCount"] = count;
            }
            

            if (id == null)
            {
                return NotFound();
            }

            var productName = await _context.ProductNames
                .Include(p => p.Manufacturer)
                .Include(p => p.ProductForm)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productName == null)
            {
                return NotFound();
            }

            string sCount = Request.Cookies[id.ToString()];
            int iCount = 0;
            if (sCount != null)
            {
                iCount = int.Parse(sCount);
            }
            if (diff != null)
            {
                iCount += (int)diff;
            }
            if(iCount < 0)
            {
                iCount = 0;
            }
            if (iCount > 0)
            {
                Response.Cookies.Append(id.ToString(), iCount.ToString());
            }
            else
            {
                Response.Cookies.Delete(id.ToString());
            }
            productName.ShoppingCartCount = iCount;

            IQueryable<ProductName> queryable = _context.ProductNames
                            .Include(p => p.Manufacturer)
                            .Include(p => p.ProductForm)
                            .Include(p => p.ProductType)
                            .Where(p => p.ProductType == productName.ProductType);
            productName.SimilarProducts = queryable.ToList<ProductName>();

            int productsNbr = _context.Products
                .Where<Product>(item => item.ProductNameId == id).ToList().Count;
            if (productsNbr == 0)
            {
                ViewData["CartOk"] = false;
                ViewData["CartChangeMessage"] = "This product is not available.";
            }
            else if(productName.ShoppingCartCount > productsNbr)
            {
                ViewData["CartOk"] = false;
                ViewData["CartChangeMessage"] = $"Max {productsNbr} available.";
            }

            return View("Details", productName);
        }

        /// <summary>
        /// w detalach - dodawanie nazwy produktu do koszyka
        /// </summary>
        /// <param name="id">id nazwy produktu</param>
        /// <returns></returns>
        public async Task<IActionResult> SubCartDetails(int? id)
        {
            return await Details(id, -1);
        }
        /// <summary>
        /// w detalach - odejmowanie nazwy produktu z koszyka
        /// </summary>
        /// <param name="id">id nazwy produktu</param>
        /// <returns></returns>
        public async Task<IActionResult> AddCartDetails(int? id)
        {
            return await Details(id, 1);
        }
    }
}
