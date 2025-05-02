// Controllers/MainCategoriesApiController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainCategoriesApiController : ControllerBase
    {
        private readonly StokContext _context;
        public MainCategoriesApiController(StokContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MainCategory>>> Get()
            => await _context.MainCategories.ToListAsync();

        [HttpPost]
        public async Task<ActionResult<MainCategory>> Post(MainCategory cat)
        {
            _context.MainCategories.Add(cat);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = cat.Id }, cat);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MainCategory cat)
        {
            if (id != cat.Id) return BadRequest();
            _context.Entry(cat).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cat = await _context.MainCategories.FindAsync(id);
            if (cat == null) return NotFound();
            _context.MainCategories.Remove(cat);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
