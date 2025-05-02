using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    public class StockExitsController : Controller
    {
        private readonly StokContext _context;
        public StockExitsController(StokContext context) => _context = context;

        public async Task<IActionResult> Index()
            => View(await _context.StockExits
                .Include(se => se.Product)
                .Include(se => se.CustomerTeam)
                .ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var se = await _context.StockExits
                .Include(s => s.Product)
                .Include(s => s.CustomerTeam)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (se == null) return NotFound();
            return View(se);
        }

        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(
                _context.Materials, "Id", "Name");
            ViewData["CustomerTeamId"] = new SelectList(
                _context.CustomerTeams, "Id", "Department");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Date,ProductId,CustomerTeamId,Quantity")]
            StockExit se)
        {
            if (ModelState.IsValid)
            {
                _context.Add(se);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(
                _context.Materials, "Id", "Name", se.ProductId);
            ViewData["CustomerTeamId"] = new SelectList(
                _context.CustomerTeams, "Id", "Department", se.CustomerTeamId);
            return View(se);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var se = await _context.StockExits.FindAsync(id);
            if (se == null) return NotFound();
            ViewData["ProductId"] = new SelectList(
                _context.Materials, "Id", "Name", se.ProductId);
            ViewData["CustomerTeamId"] = new SelectList(
                _context.CustomerTeams, "Id", "Department", se.CustomerTeamId);
            return View(se);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Date,ProductId,CustomerTeamId,Quantity")]
            StockExit se)
        {
            if (id != se.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = new SelectList(
                    _context.Materials, "Id", "Name", se.ProductId);
                ViewData["CustomerTeamId"] = new SelectList(
                    _context.CustomerTeams, "Id", "Department", se.CustomerTeamId);
                return View(se);
            }
            try
            {
                _context.Update(se);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.StockExits.Any(e => e.Id == se.Id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var se = await _context.StockExits
                .Include(s => s.Product)
                .Include(s => s.CustomerTeam)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (se == null) return NotFound();
            return View(se);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var se = await _context.StockExits.FindAsync(id);
            _context.StockExits.Remove(se);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
