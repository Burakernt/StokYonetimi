
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using StokYonetimiNew.Data;
    using StokYonetimiNew.Models;

    namespace StokYonetimiNew.Controllers
    {
        public class MainCategoriesController : Controller
        {
            private readonly StokContext _context;
            public MainCategoriesController(StokContext context) => _context = context;

            public async Task<IActionResult> Index()
                => View(await _context.MainCategories.ToListAsync());

            public async Task<IActionResult> Details(int? id)
            {
                if (id == null) return NotFound();
                var mc = await _context.MainCategories
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (mc == null) return NotFound();
                return View(mc);
            }

            public IActionResult Create() => View();

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(
                [Bind("Code,Name")] MainCategory mc)
            {
                if (!ModelState.IsValid) return View(mc);
                _context.Add(mc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null) return NotFound();
                var mc = await _context.MainCategories.FindAsync(id);
                if (mc == null) return NotFound();
                return View(mc);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id,
                [Bind("Id,Code,Name")] MainCategory mc)
            {
                if (id != mc.Id) return NotFound();
                if (!ModelState.IsValid) return View(mc);
                try
                {
                    _context.Update(mc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.MainCategories.Any(e => e.Id == mc.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null) return NotFound();
                var mc = await _context.MainCategories
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (mc == null) return NotFound();
                return View(mc);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var mc = await _context.MainCategories.FindAsync(id);
                _context.MainCategories.Remove(mc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
    }

