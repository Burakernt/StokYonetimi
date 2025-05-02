// Controllers/SubCategoriesApiController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesApiController : ControllerBase
    {
        private readonly StokContext _context;
        public SubCategoriesApiController(StokContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategory>>> Get()
            => await _context.SubCategories.ToListAsync();

        [HttpPost]
        public async Task<ActionResult<SubCategory>> Post(SubCategory sub)
        {
            _context.SubCategories.Add(sub);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = sub.Id }, sub);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, SubCategory sub)
        {
            if (id != sub.Id) return BadRequest();
            _context.Entry(sub).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sub = await _context.SubCategories.FindAsync(id);
            if (sub == null) return NotFound();
            _context.SubCategories.Remove(sub);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
