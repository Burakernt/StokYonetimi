using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Filters;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // Sadece Admin kullanıcılar CRUD yapabilir
    [RequireLogin(Roles = new[] { UserRole.Admin })]
    public class MeasurementUnitsApiController : ControllerBase
    {
        private readonly StokContext _db;
        public MeasurementUnitsApiController(StokContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _db.MeasurementUnits.OrderBy(u => u.Name).ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MeasurementUnit u)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _db.MeasurementUnits.Add(u);
            await _db.SaveChangesAsync();
            return Ok(u);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MeasurementUnit u)
        {
            if (id != u.Id) return BadRequest();
            if (!await _db.MeasurementUnits.AnyAsync(x => x.Id == id))
                return NotFound();
            _db.Entry(u).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var u = await _db.MeasurementUnits.FindAsync(id);
            if (u == null) return NotFound();
            _db.MeasurementUnits.Remove(u);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
