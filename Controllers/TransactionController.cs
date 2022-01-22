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

            var shippingData = _context.ShippingData
                .Where(sd => sd.UserId == userId)
                .First();

            var allCartIds = Request.Cookies.Select(item => item.Key).ToList();

            var selectedArticles = _context.ProductNames
                .Where(pn => allCartIds.Contains(pn.Id.ToString()))
                .ToListAsync();

            double totalPrice = 0;
            foreach(var prod in await selectedArticles)
            {
                totalPrice += prod.Price * Double.Parse(Request.Cookies[prod.Id.ToString()]);
            }

            ViewData["Summary"] = totalPrice;

            return View((userData, new TransactionData(), shippingData));
        }
    }
}
