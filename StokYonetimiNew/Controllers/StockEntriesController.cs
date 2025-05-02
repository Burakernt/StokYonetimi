using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    public class StockEntriesController : Controller
    {
        private readonly StokContext _context;
        public StockEntriesController(StokContext context) => _context = context;

        public async Task<IActionResult> Index()
            => View(await _context.StockEntries
                .Include(se => se.Supplier)
                .Include(se => se.Product)
                .ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var se = await _context.StockEntries
                .Include(s => s.Supplier)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (se == null) return NotFound();
            return View(se);
        }

        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(
                _context.Suppliers, "Id", "ShortName");
            ViewData["ProductId"] = new SelectList(
                _context.Materials, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Date,InvoiceNo,SupplierId,ProductId,Quantity,UnitPrice")]
            StockEntry se)
        {
            if (ModelState.IsValid)
            {
                _context.Add(se);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(
                _context.Suppliers, "Id", "ShortName", se.SupplierId);
            ViewData["ProductId"] = new SelectList(
                _context.Materials, "Id", "Name", se.ProductId);
            return View(se);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var se = await _context.StockEntries.FindAsync(id);
            if (se == null) return NotFound();
            ViewData["SupplierId"] = new SelectList(
                _context.Suppliers, "Id", "ShortName", se.SupplierId);
            ViewData["ProductId"] = new SelectList(
                _context.Materials, "Id", "Name", se.ProductId);
            return View(se);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Date,InvoiceNo,SupplierId,ProductId,Quantity,UnitPrice")]
            StockEntry se)
        {
            if (id != se.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                ViewData["SupplierId"] = new SelectList(
                    _context.Suppliers, "Id", "ShortName", se.SupplierId);
                ViewData["ProductId"] = new SelectList(
                    _context.Materials, "Id", "Name", se.ProductId);
                return View(se);
            }
            try
            {
                _context.Update(se);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.StockEntries.Any(e => e.Id == se.Id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var se = await _context.StockEntries
                .Include(s => s.Supplier)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (se == null) return NotFound();
            return View(se);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var se = await _context.StockEntries.FindAsync(id);
            _context.StockEntries.Remove(se);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
