using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using StokYonetimiNew.Data;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    public class SubCategoriesController : Controller
    {
        private readonly StokContext _context;
        public SubCategoriesController(StokContext context) => _context = context;

        // GET: SubCategories
        public async Task<IActionResult> Index()
            => View(await _context.SubCategories
                .Include(s => s.MainCategory)
                .ToListAsync());

        // GET: Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var sc = await _context.SubCategories
                .Include(s => s.MainCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sc == null) return NotFound();
            return View(sc);
        }

        // GET: Create
        public IActionResult Create()
        {
            ViewData["MainCategoryId"] = new SelectList(
                _context.MainCategories, "Id", "Name");
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Code,Name,MainCategoryId")] SubCategory sc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MainCategoryId"] = new SelectList(
                _context.MainCategories, "Id", "Name", sc.MainCategoryId);
            return View(sc);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var sc = await _context.SubCategories.FindAsync(id);
            if (sc == null) return NotFound();
            ViewData["MainCategoryId"] = new SelectList(
                _context.MainCategories, "Id", "Name", sc.MainCategoryId);
            return View(sc);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Code,Name,MainCategoryId")] SubCategory sc)
        {
            if (id != sc.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                ViewData["MainCategoryId"] = new SelectList(
                    _context.MainCategories, "Id", "Name", sc.MainCategoryId);
                return View(sc);
            }
            try
            {
                _context.Update(sc);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.SubCategories.Any(e => e.Id == sc.Id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var sc = await _context.SubCategories
                .Include(s => s.MainCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sc == null) return NotFound();
            return View(sc);
        }

        // POST: DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sc = await _context.SubCategories.FindAsync(id);
            _context.SubCategories.Remove(sc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
