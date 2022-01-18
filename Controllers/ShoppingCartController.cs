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

        public ShoppingCartController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: ShoppingCartController
        [Route("ShoppingCart")]
        public async Task<IActionResult> Index()
        {
            var allCartIds = Request.Cookies.Select(item => item.Key).ToList();

            var allCartArticles = _context.ProductNames
                .Where<ProductName>(item => allCartIds.Contains(item.Id.ToString()))
                ;

            foreach(var article in allCartArticles)
            {
                article.ShoppingCartCount = Int32.Parse(Request.Cookies[article.Id.ToString()]);
                article.ShoppingCartSumPrice = article.Price * article.ShoppingCartCount;
            }

            return View(await allCartArticles.ToListAsync());
        }

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

        public async Task<IActionResult> SubCart(int? id)
        {
            string sCount = Request.Cookies[id.ToString()];
            int iCount = 1;
            iCount = int.Parse(sCount);
            iCount -= 1;

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

        public async Task<IActionResult> DelCart(int? id)
        {
            Response.Cookies.Delete(id.ToString());
            return RedirectToAction("");
        }
    }
}
