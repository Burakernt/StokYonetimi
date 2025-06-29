using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using StokYonetimiNew.Data;
using StokYonetimiNew.Filters;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    [RoleAuthorize(UserRole.Admin)]
    public class CustomerTeamsController : Controller
    {
        private readonly StokContext _context;
        public CustomerTeamsController(StokContext context) => _context = context;

        public async Task<IActionResult> Index()
            => View(await _context.CustomerTeams.ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var t = await _context.CustomerTeams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (t == null) return NotFound();
            return View(t);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Department,TeamName")] CustomerTeam t)
        {
            if (!ModelState.IsValid) return View(t);
            _context.Add(t);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var t = await _context.CustomerTeams.FindAsync(id);
            if (t == null) return NotFound();
            return View(t);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Department,TeamName")] CustomerTeam t)
        {
            if (id != t.Id) return NotFound();
            if (!ModelState.IsValid) return View(t);
            try
            {
                _context.Update(t);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.CustomerTeams.Any(e => e.Id == t.Id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var t = await _context.CustomerTeams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (t == null) return NotFound();
            return View(t);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var t = await _context.CustomerTeams.FindAsync(id);
            _context.CustomerTeams.Remove(t);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
