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
            var firstPrescription = new Prescription();
            if(addedPrescriptions.Count() != 0)
            {
                firstPrescription = addedPrescriptions.First();
                firstPrescription.PrescriptionList = addedPrescriptions;
            }
            firstPrescription.Code = -1;

            return View(firstPrescription);
        }

        public IActionResult SearchPrescription()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchPrescription([Bind("Code,Pesel")] Prescription pres)
        {
            var addedPrescriptions = await GetStoredPrescriptions();
            if (pres.Code.ToString().StartsWith("9"))
            {
                ViewData["NoPrescription"] = "Nie znaleziono żadnych recept 😢";
                var first = addedPrescriptions.First();
                first.PrescriptionList = addedPrescriptions;
                first.Code = -1;

                return View("Index", first);
            }
            
            var generatedPrescription = new Prescription
            {
                PrescriptionCode = pres.Code,
                StartDate = DateTime.Now.AddDays(new Random().Next(0, 10)),
                EndDate = DateTime.Now.AddDays(new Random().Next(20, 41))
            };
            generatedPrescription.PrescriptionList = addedPrescriptions;
            return View("Index", generatedPrescription);
        }

        public async Task<IActionResult> AddPrescription(int? code, string startdate, string enddate)
        {
            var newPrescription = new Prescription();
            if (!code.HasValue)
            {
                return RedirectToAction(nameof(Index));
            }
            var startDate = DateTime.Parse(startdate);
            var endDate = DateTime.Parse(enddate);
            newPrescription.StartDate = startDate;
            newPrescription.EndDate = endDate;
            newPrescription.PrescriptionCode = code.Value;
            newPrescription.UserId = userId;

            _context.Prescriptions.Add(newPrescription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> RemovePrescription(int? id)
        {
            Console.WriteLine(id);
            if(id == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescriptions.FirstOrDefaultAsync(p => p.Id == id);
            if (prescription == null)
            {
                return NotFound();
            }

            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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
