using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Models;


namespace StokYonetimiNew.Controllers
{
    public class ProductTypesController : Controller
    {
        private readonly StokContext _context;
        public ProductTypesController(StokContext context) => _context = context;

        public async Task<IActionResult> Index()
            => View(await _context.ProductTypes
                .Include(pt => pt.SubCategory)
                .ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var pt = await _context.ProductTypes
                .Include(pt => pt.SubCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pt == null) return NotFound();
            return View(pt);
        }

        public IActionResult Create()
        {
            ViewData["SubCategoryId"] = new SelectList(
                _context.SubCategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Code,Name,SubCategoryId")] ProductType pt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubCategoryId"] = new SelectList(
                _context.SubCategories, "Id", "Name", pt.SubCategoryId);
            return View(pt);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var pt = await _context.ProductTypes.FindAsync(id);
            if (pt == null) return NotFound();
            ViewData["SubCategoryId"] = new SelectList(
                _context.SubCategories, "Id", "Name", pt.SubCategoryId);
            return View(pt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Code,Name,SubCategoryId")] ProductType pt)
        {
            if (id != pt.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                ViewData["SubCategoryId"] = new SelectList(
                    _context.SubCategories, "Id", "Name", pt.SubCategoryId);
                return View(pt);
            }
            try
            {
                _context.Update(pt);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ProductTypes.Any(e => e.Id == pt.Id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var pt = await _context.ProductTypes
                .Include(pt => pt.SubCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pt == null) return NotFound();
            return View(pt);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pt = await _context.ProductTypes.FindAsync(id);
            _context.ProductTypes.Remove(pt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
