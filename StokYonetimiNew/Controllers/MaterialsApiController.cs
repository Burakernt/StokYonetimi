using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Filters;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsApiController : ControllerBase
    {
        private readonly StokContext _context;
        public MaterialsApiController(StokContext context) => _context = context;
        [RequireLogin(Roles = new[] { UserRole.Admin, UserRole.Reporter })]

        // GET: api/MaterialsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material>>> Get()
        {
            // İfade ağacına uygun, sade expression lambdaları kullandık.
            var materials = await _context.Materials
                .Include(m => m.MaterialType)
                .Include(m => m.Supplier)
                .ToListAsync();
            return Ok(materials);
        }
        [RequireLogin(Roles = new[] { UserRole.Admin })]

        // POST: api/MaterialsApi
        [HttpPost]
        public async Task<ActionResult<Material>> Post([FromBody] Material m)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Materials.Add(m);
            await _context.SaveChangesAsync();
            // CreatedAtAction ile 201 dönüyoruz
            return CreatedAtAction(nameof(Get), new { id = m.Id }, m);
        }
        [RequireLogin(Roles = new[] { UserRole.Admin })]

        // PUT: api/MaterialsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Material m)
        {
            if (id != m.Id)
                return BadRequest("ID uyuşmuyor");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(m).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Materials.AnyAsync(x => x.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }
        [RequireLogin(Roles = new[] { UserRole.Admin })]

        // DELETE: api/MaterialsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var m = await _context.Materials.FindAsync(id);
            if (m == null)
                return NotFound();

            _context.Materials.Remove(m);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
