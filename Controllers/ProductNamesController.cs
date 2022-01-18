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

        public ProductNamesController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: ProductNames
        public async Task<IActionResult> Index()
        {
            var shopDbContext = _context.ProductNames.Include(p => p.Manufacturer).Include(p => p.ProductForm).Include(p => p.ProductType);
            return View(await shopDbContext.ToListAsync());
        }

        // GET: ProductNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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

            return View(productName);
        }

        // GET: ProductNames/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            ViewData["ProductFormId"] = new SelectList(_context.Set<ProductForm>(), "Id", "Name");
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "Id", "Name");
            return View();
        }

        // POST: ProductNames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,RequiresPrescription,Description,ManufacturerId,ImageFilename,ProductFormId,ProductTypeId")] ProductName productName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", productName.ManufacturerId);
            ViewData["ProductFormId"] = new SelectList(_context.Set<ProductForm>(), "Id", "Name", productName.ProductFormId);
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "Id", "Name", productName.ProductTypeId);
            return View(productName);
        }

        // GET: ProductNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productName = await _context.ProductNames.FindAsync(id);
            if (productName == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", productName.ManufacturerId);
            ViewData["ProductFormId"] = new SelectList(_context.Set<ProductForm>(), "Id", "Name", productName.ProductFormId);
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "Id", "Name", productName.ProductTypeId);
            return View(productName);
        }

        // POST: ProductNames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,RequiresPrescription,Description,ManufacturerId,ImageFilename,ProductFormId,ProductTypeId")] ProductName productName)
        {
            if (id != productName.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductNameExists(productName.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", productName.ManufacturerId);
            ViewData["ProductFormId"] = new SelectList(_context.Set<ProductForm>(), "Id", "Name", productName.ProductFormId);
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "Id", "Name", productName.ProductTypeId);
            return View(productName);
        }

        // GET: ProductNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
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

            return View(productName);
        }

        // POST: ProductNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productName = await _context.ProductNames.FindAsync(id);
            _context.ProductNames.Remove(productName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductNameExists(int id)
        {
            return _context.ProductNames.Any(e => e.Id == id);
        }
    }
}
