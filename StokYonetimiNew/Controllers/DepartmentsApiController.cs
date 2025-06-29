using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Filters;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsApiController : ControllerBase
    {
        private readonly StokContext _context;
        public DepartmentsApiController(StokContext ctx) => _context = ctx;
        [RequireLogin(Roles = new[] { UserRole.Admin, UserRole.Reporter })]

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> Get()
            => Ok(await _context.Departments.ToListAsync());
        [RequireLogin(Roles = new[] { UserRole.Admin })]

        [HttpPost]
        public async Task<ActionResult<Department>> Post(Department d)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Departments.Add(d);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = d.Id }, d);
        }
        [RequireLogin(Roles = new[] { UserRole.Admin })]

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Department d)
        {
            if (id != d.Id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Entry(d).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [RequireLogin(Roles = new[] { UserRole.Admin })]

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var d = await _context.Departments.FindAsync(id);
            if (d == null) return NotFound();
            _context.Departments.Remove(d);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}