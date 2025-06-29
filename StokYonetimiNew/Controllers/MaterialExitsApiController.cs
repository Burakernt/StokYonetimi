using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Filters;
using StokYonetimiNew.Models;
using System;
using System.Collections.Generic;
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

        [RequireLogin(Roles = new[] { UserRole.Admin, UserRole.Reporter })]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialExit>>> Get()
        {
            var list = await _context.MaterialExits
                .Include(e => e.CustomerTeam)
                .Include(e => e.Material)
                .ToListAsync();
            return Ok(list);
        }

        [RequireLogin(Roles = new[] { UserRole.Admin })]
        [HttpPost]
        public async Task<ActionResult<MaterialExit>> Post([FromBody] MaterialExit exit)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // stok kontrolü
            var totalIn = await _context.MaterialEntries
                .Where(e => e.MaterialId == exit.MaterialId)
                .SumAsync(e => (int?)e.Quantity) ?? 0;
            var totalOut = await _context.MaterialExits
                .Where(e => e.MaterialId == exit.MaterialId)
                .SumAsync(e => (int?)e.Quantity) ?? 0;
            if (totalOut + exit.Quantity > totalIn)
                return BadRequest($"Yetersiz stok. Kalan: {totalIn - totalOut}");

            exit.Date = DateTime.SpecifyKind(exit.Date, DateTimeKind.Utc);
            _context.MaterialExits.Add(exit);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = exit.Id }, exit);
        }

        [RequireLogin(Roles = new[] { UserRole.Admin })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MaterialExit updated)
        {
            if (id != updated.Id)
                return BadRequest("ID uyuşmuyor.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Mevcut çıkış kaydını bul
            var existing = await _context.MaterialExits.FindAsync(id);
            if (existing == null)
                return NotFound();

            // stok kontrol: önce tüm çıkışlar, sonra bu kaydı çık
            var totalIn = await _context.MaterialEntries
                .Where(e => e.MaterialId == updated.MaterialId)
                .SumAsync(e => (int?)e.Quantity) ?? 0;
            var totalOutExcludingThis = await _context.MaterialExits
                .Where(e => e.MaterialId == updated.MaterialId && e.Id != id)
                .SumAsync(e => (int?)e.Quantity) ?? 0;
            if (totalOutExcludingThis + updated.Quantity > totalIn)
                return BadRequest($"Yetersiz stok. Kalan: {totalIn - totalOutExcludingThis}");

            // alanları güncelle
            existing.Date = DateTime.SpecifyKind(updated.Date, DateTimeKind.Utc);
            existing.MaterialId = updated.MaterialId;
            existing.Quantity = updated.Quantity;
            existing.CustomerTeamId = updated.CustomerTeamId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [RequireLogin(Roles = new[] { UserRole.Admin })]
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
