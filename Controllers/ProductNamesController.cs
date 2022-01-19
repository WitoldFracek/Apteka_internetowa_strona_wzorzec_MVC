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
        public async Task<IActionResult> Index(int? ProductTypeId, int? ProductFormId, int? ManufacturerId, string? SearchContent, string? PrescriptionValue, int? SorterId)
        {
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

            var shopContextFiltered = _context.ProductNames.Select(a => a);
            if(ProductTypeId != null)
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
            if (SorterId == 0)
            {
                shopContextFiltered = shopContextFiltered.OrderByDescending(item => item.Price);
            }
            if (SorterId == 1)
            {
                shopContextFiltered = shopContextFiltered.OrderBy(item => item.Price);
            }
            if (SearchContent != null && SearchContent != "")
            {
                SearchContent = SearchContent.Replace('+', ' ');
                ViewData["SearchContent"] = SearchContent;
                shopContextFiltered = shopContextFiltered.Where<ProductName>(item => item.Name.Contains(SearchContent));
            }
            if (PrescriptionValue == "true")
            {
                shopContextFiltered = shopContextFiltered.Where<ProductName>(item => item.RequiresPrescription == true);
                ViewData["Prescription"] = "true";
            }
            else
            {
                shopContextFiltered = shopContextFiltered.Where<ProductName>(item => item.RequiresPrescription == false);
                ViewData["Prescription"] = "false";
            }
            return View(await shopContextFiltered.ToListAsync());
        }

        // GET: ProductNames/Details/5
        public async Task<IActionResult> Details(int? id, int? diff)
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

            return View("Details", productName);
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

        public async Task<IActionResult> SubCartDetails(int? id)
        {
            return await Details(id, -1);
        }
        public async Task<IActionResult> AddCartDetails(int? id)
        {
            return await Details(id, 1);
        }
    }
}
