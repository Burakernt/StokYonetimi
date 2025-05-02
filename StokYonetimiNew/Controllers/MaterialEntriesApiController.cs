using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialEntriesApiController : ControllerBase
    {
        private readonly StokContext _context;
        public MaterialEntriesApiController(StokContext context) => _context = context;

        // GET: api/MaterialEntriesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialEntry>>> Get()
        {
            var list = await _context.MaterialEntries
                .Include(e => e.Supplier)
                .Include(e => e.Material)
                .ToListAsync();
            return Ok(list);
        }

        // POST: api/MaterialEntriesApi
        [HttpPost]
        public async Task<ActionResult<MaterialEntry>> Post([FromBody] MaterialEntry e)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // DateTimeKind=Utc olacak şekilde dönüştürüyoruz
            e.Date = DateTime.SpecifyKind(e.Date, DateTimeKind.Utc);

            _context.MaterialEntries.Add(e);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = e.Id }, e);
        }

        // DELETE: api/MaterialEntriesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _context.MaterialEntries.FindAsync(id);
            if (e == null) return NotFound();
            _context.MaterialEntries.Remove(e);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
