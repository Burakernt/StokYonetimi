using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StokYonetimiNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialExitsApiController : ControllerBase
    {
        private readonly StokContext _context;
        public MaterialExitsApiController(StokContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialExit>>> Get()
        {
            var list = await _context.MaterialExits
                .Include(e => e.CustomerTeam)
                .Include(e => e.Material)
                .ToListAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult<MaterialExit>> Post([FromBody] MaterialExit exit)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 1) Toplam giriş miktarı
            var totalIn = await _context.MaterialEntries
                .Where(e => e.MaterialId == exit.MaterialId)
                .SumAsync(e => (int?)e.Quantity) ?? 0;
            // 2) Toplam çıkış miktarı
            var totalOut = await _context.MaterialExits
                .Where(e => e.MaterialId == exit.MaterialId)
                .SumAsync(e => (int?)e.Quantity) ?? 0;
            // 3) Stok kontrolü
            if (totalOut + exit.Quantity > totalIn)
                return BadRequest($"Yetersiz stok. Kalan: {totalIn - totalOut}");

            exit.Date = DateTime.SpecifyKind(exit.Date, DateTimeKind.Utc);

            _context.MaterialExits.Add(exit);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = exit.Id }, exit);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _context.MaterialExits.FindAsync(id);
            if (e == null) return NotFound();
            _context.MaterialExits.Remove(e);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
