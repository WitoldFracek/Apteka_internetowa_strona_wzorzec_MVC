using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PO_Projekt.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PO_Projekt.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PO_Projekt.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShopDbContext _context;

        /// <summary>
        /// Controller do obsługi zawartości koszyka.
        /// </summary>
        /// <param name="context"></param>
        public ShoppingCartController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: ShoppingCartController
        /// <summary>
        /// Wyświetlanie całej zawartości koszyka zczytywanych z cookies.
        /// </summary>
        [Route("ShoppingCart")]
        public async Task<IActionResult> Index()
        {
            var allCartIds = Request.Cookies.Select(item => item.Key).ToList();

            var allCartArticles = _context.ProductNames
                .Where<ProductName>(item => allCartIds.Contains(item.Id.ToString()))
                ;
            if (allCartArticles.ToList().Count == 0)
            {
                return View("CartEmpty", _context.ProductNames);
            }
            int count = 0;
            foreach(var article in allCartArticles)
            {
                article.ShoppingCartCount = Int32.Parse(Request.Cookies[article.Id.ToString()]);
                article.ShoppingCartSumPrice = article.Price * article.ShoppingCartCount;
                count += article.ShoppingCartCount;
                article.AvailableAmount = 0;
            }
            ViewData["CartCount"] = count;
            var products = _context.Products
                .Where<Product>(item => allCartIds.Contains(item.ProductNameId.ToString()));
            var availableSum = 0;
            foreach(var product in products)
            {
                var firstItem = allCartArticles
                    .Where<ProductName>(item => item.Id == product.ProductNameId)
                    .FirstOrDefault<ProductName>();
                if(firstItem != null)
                {
                    firstItem.AvailableAmount += 1;
                    availableSum += 1;
                }
            }
            foreach(var pn in allCartArticles)
            {
                if(pn.AvailableAmount < pn.ShoppingCartCount)
                {
                    ViewData["CartOk"] = false;
                    ViewData["CartChangeMessage"] = "Some of the products added to shopping cart are unavailable.";
                }
            }

            return View(await allCartArticles.ToListAsync());
        }

        /// <summary>
        /// Dodawanie danej nazwy produktu do koszyka, odświerzenie koszyka.
        /// </summary>
        /// <param name="id">Id nazwy produktu.</param>
        /// <returns></returns>
        public async Task<IActionResult> AddCart(int? id)
        {
            string sCount = Request.Cookies[id.ToString()];
            int iCount = 0;
            if (sCount != null)
            {
                iCount = int.Parse(sCount);
            }
            iCount += 1;
            Response.Cookies.Append(id.ToString(), iCount.ToString());
            return RedirectToAction("");
        }

        /// <summary>
        /// Dodawanie danej nazwy produktu do koszyka, powrót do strony poprzedniej.
        /// </summary>
        /// <param name="id">Id nazwy produktu.</param>
        /// <returns></returns>
        public async Task<IActionResult> AddCartRedirect(int? id)
        {
            string sCount = Request.Cookies[id.ToString()];
            int iCount = 0;
            if (sCount != null)
            {
                iCount = int.Parse(sCount);
            }
            iCount += 1;

            Response.Cookies.Append(id.ToString(), iCount.ToString());
            return View("AddCartRedirect");
        }

        /// <summary>
        /// Odejmowanie nazwy produktu z koszyka, odświerzanie koszyka.
        /// </summary>
        /// <param name="id">Id nazwy produktu.</param>
        /// <returns></returns>
        public async Task<IActionResult> SubCart(int? id)
        {
            string sCount = Request.Cookies[id.ToString()];
            int iCount = int.Parse(sCount) - 1;
            if (iCount > 0)
            {
                Response.Cookies.Append(id.ToString(), iCount.ToString());
            }
            else
            {
                Response.Cookies.Delete(id.ToString());
            }
            return RedirectToAction("");
        }

        /// <summary>
        /// Odejmowanie danej nazwy produktu z koszyka, powrót do strony poprzedniej.
        /// </summary>
        /// <param name="id">Id nazwy produktu.</param>
        /// <returns></returns>
        public async Task<IActionResult> SubCartRedirect(int? id)
        {
            string sCount = Request.Cookies[id.ToString()];
            int iCount = 0;
            if(sCount != null)
            {
                iCount = int.Parse(sCount);
            }
            iCount -= 1;
            if (iCount > 0)
            {
                Response.Cookies.Append(id.ToString(), iCount.ToString());
            }
            else
            {
                Response.Cookies.Delete(id.ToString());
            }
            return View("AddCartRedirect");
        }

        /// <summary>
        /// Usuwanie danej nazwy produktu z koszyka, odświerzanie koszyka.
        /// </summary>
        /// <param name="id">Id nazwy produktu.</param>
        /// <returns></returns>
        public async Task<IActionResult> DelCart(int? id)
        {
            Response.Cookies.Delete(id.ToString());
            return RedirectToAction("");
        }
    }
}