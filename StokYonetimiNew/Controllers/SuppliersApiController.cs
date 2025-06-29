using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Filters;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SuppliersApiController : ControllerBase
    {
        private readonly StokContext _context;
        public SuppliersApiController(StokContext context) => _context = context;
        [RequireLogin(Roles = new[] { UserRole.Admin, UserRole.Reporter })]

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> Get()
            => await _context.Suppliers.ToListAsync();
        [RequireLogin(Roles = new[] { UserRole.Admin })]

        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> Get(int id)
        {
            var s = await _context.Suppliers.FindAsync(id);
            if (s == null) return NotFound();
            return s;
        }
        [RequireLogin(Roles = new[] { UserRole.Admin })]

        [HttpPost]
        public async Task<ActionResult<Supplier>> Post(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = supplier.Id }, supplier);
        }
        [RequireLogin(Roles = new[] { UserRole.Admin })]

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Supplier supplier)
        {
            if (id != supplier.Id) return BadRequest();
            _context.Entry(supplier).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [RequireLogin(Roles = new[] { UserRole.Admin })]

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var s = await _context.Suppliers.FindAsync(id);
            if (s == null) return NotFound();
            _context.Suppliers.Remove(s);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
