using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    public class UnitsController : Controller
    {
        private readonly StokContext _context;
        public UnitsController(StokContext context) => _context = context;

        public async Task<IActionResult> Index()
            => View(await _context.Units.ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var u = await _context.Units
                .FirstOrDefaultAsync(m => m.Id == id);
            if (u == null) return NotFound();
            return View(u);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name")] Unit u)
        {
            if (!ModelState.IsValid) return View(u);
            _context.Add(u);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var u = await _context.Units.FindAsync(id);
            if (u == null) return NotFound();
            return View(u);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Name")] Unit u)
        {
            if (id != u.Id) return NotFound();
            if (!ModelState.IsValid) return View(u);
            try
            {
                _context.Update(u);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Units.Any(e => e.Id == u.Id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var u = await _context.Units
                .FirstOrDefaultAsync(m => m.Id == id);
            if (u == null) return NotFound();
            return View(u);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var u = await _context.Units.FindAsync(id);
            _context.Units.Remove(u);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
