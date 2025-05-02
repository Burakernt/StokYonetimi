using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypesApiController : ControllerBase
    {
        private readonly StokContext _context;
        public ProductTypesApiController(StokContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductType>>> Get()
            => await _context.ProductTypes.ToListAsync();

        [HttpPost]
        public async Task<ActionResult<ProductType>> Post([FromBody] ProductType pt)
        {
            var entity = new ProductType
            {
                Code = pt.Code,
                Name = pt.Name,
                SubCategoryId = pt.SubCategoryId
            };
            _context.ProductTypes.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductType pt)
        {
            if (id != pt.Id) return BadRequest();
            // yalnızca gerekli üç alanı güncelle
            var entity = await _context.ProductTypes.FindAsync(id);
            if (entity == null) return NotFound();
            entity.Code = pt.Code;
            entity.Name = pt.Name;
            entity.SubCategoryId = pt.SubCategoryId;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pt = await _context.ProductTypes.FindAsync(id);
            if (pt == null) return NotFound();
            _context.ProductTypes.Remove(pt);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
