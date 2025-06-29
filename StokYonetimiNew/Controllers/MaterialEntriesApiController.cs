using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Filters;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialEntriesApiController : ControllerBase
    {
        private readonly StokContext _context;
        public MaterialEntriesApiController(StokContext context) => _context = context;
        [RequireLogin(Roles = new[] { UserRole.Admin, UserRole.Reporter })]

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
        [RequireLogin(Roles = new[] { UserRole.Admin })]

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
        [RequireLogin(Roles = new[] { UserRole.Admin })]

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
        
        [RequireLogin(Roles = new[] { UserRole.Admin })]
        // PUT: api/MaterialEntriesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MaterialEntry updated)
        {
            if (id != updated.Id)
                return BadRequest("ID uyuşmuyor.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Var mı diye kontrol et
            var existing = await _context.MaterialEntries.FindAsync(id);
            if (existing == null)
                return NotFound();

            // Güncellenecek alanları kopyala
            existing.Date = DateTime.SpecifyKind(updated.Date, DateTimeKind.Utc);
            existing.InvoiceNumber = updated.InvoiceNumber;
            existing.SupplierId = updated.SupplierId;
            existing.MaterialId = updated.MaterialId;
            existing.Quantity = updated.Quantity;
            existing.UnitPrice = updated.UnitPrice;
            existing.KDV = updated.KDV;

            // Değişiklikleri kaydet
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
