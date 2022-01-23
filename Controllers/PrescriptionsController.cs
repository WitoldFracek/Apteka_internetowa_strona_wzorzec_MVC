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
        /// This is a constructor for PrescriptionController
        /// </summary>
        /// <param name="context">This parameter is a database context that allows the controller to access database objects</param>
        public PrescriptionsController(ShopDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// An async method that access data from database via context and redirects them into the Prescription Index View
        /// </summary>
        /// <returns>Prescription Index view of MVC structure</returns>
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

        //public IActionResult SearchPrescription()
        //{
        //    return View();
        //}

        /// <summary>
        /// An async method that fetches prescription data specified by binded parameters of Prescription Class.
        /// </summary>
        /// <param name="pres">Prescription object with binded Code and Pesel parameters that is used to search the outsode prescription database for information about client prescriptions </param>
        /// <returns>Redirects to Prescription Index View with received data from outsode database.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchPrescription([Bind("Code,Pesel")] Prescription pres)
        {
            var addedPrescriptions = await GetStoredPrescriptions();
            if (pres.Code.ToString().StartsWith("9") || pres.Code <= 0)
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

        /// <summary>
        /// An async method that adds a new prescription to the database.
        /// </summary>
        /// <param name="code">Prescription code analogous to the real prescription code.</param>
        /// <param name="startdate">Starting date from which the prescription is valid</param>
        /// <param name="enddate">Prescripion expiary date</param>
        /// <returns>Redirects to Prescription Index View</returns>
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


        /// <summary>
        /// An async method that removes the prescription specified by index from the local database. It does not affect prescriptions from outside source.
        /// </summary>
        /// <param name="id">Id of the prescription to be removed.</param>
        /// <returns>Redirects to Prescription Index View</returns>
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

        /// <summary>
        /// A private async method that returns the list of prescriptions for a specified, logged in user by their id.
        /// </summary>
        /// <returns>Returns a list of not terminated prescriptions stored in the local database.</returns>
        private async Task<List<Prescription>> GetStoredPrescriptions()
        {
            var addedPrescriptions = await _context.Prescriptions
                .Include(p => p.User)
                .Where(p => p.UserId == userId).ToListAsync();

            var terminatedPrescriptions = addedPrescriptions.Where(p => p.EndDate < DateTime.Now).ToList();
            RemoveTerminated(terminatedPrescriptions);

            return addedPrescriptions.Where(p => p.EndDate >= DateTime.Now).ToList();
        }

        /// <summary>
        /// A private aync method that is used to remove expired prescriptions from the local database.
        /// </summary>
        /// <param name="terminated">The list of expired prescriptions to be removed.</param>
        private async void RemoveTerminated(List<Prescription> terminated)
        {
            foreach(var pres in terminated)
            {
                _context.Prescriptions.Remove(pres);
            }
            await _context.SaveChangesAsync();
        }
    }
}
