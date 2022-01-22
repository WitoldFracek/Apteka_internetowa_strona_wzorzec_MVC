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
    public class PrescriptionsController : Controller
    {
        private readonly ShopDbContext _context;
        private readonly int userId = 1;

        public PrescriptionsController(ShopDbContext context)
        {
            _context = context;
        }

        [Route("Prescriptions")]
        public async Task<IActionResult> Index()
        {
            var addedPrescriptions = await GetStoredPrescriptions();

            return View((addedPrescriptions, new SearchPrescriptionData(), new Prescription() { Id = -1}));
        }

        public IActionResult SearchPrescription()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchPrescription([Bind("Id,Pesel")] SearchPrescriptionData presData)
        {
            Console.WriteLine(presData.Id);
            Console.WriteLine(presData.Pesel);
            var addedPrescriptions = await GetStoredPrescriptions();
            var generatedPrescription = new Prescription
            {
                PrescriptionCode = presData.Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(new Random().Next(10, 21))
            };
            return View("Index", (addedPrescriptions, presData, generatedPrescription));
        }

        private async Task<List<Prescription>> GetStoredPrescriptions()
        {
            var addedPrescriptions = await _context.Prescriptions
                .Include(p => p.User)
                .Where(p => p.UserId == userId).ToListAsync();

            var terminatedPrescriptions = addedPrescriptions.Where(p => p.EndDate < DateTime.Now).ToList();
            RemoveTerminated(terminatedPrescriptions);

            return addedPrescriptions.Where(p => p.EndDate >= DateTime.Now).ToList();
        }

        private void RemoveTerminated(List<Prescription> terminated)
        {

        }
    }
}
