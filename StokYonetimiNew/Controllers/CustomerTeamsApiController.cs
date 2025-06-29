using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Filters;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTeamsApiController : ControllerBase
    {
        private readonly StokContext _context;
        public CustomerTeamsApiController(StokContext context) => _context = context;

        // GET api/CustomerTeamsApi
        [RequireLogin(Roles = new[] { UserRole.Admin })]

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerTeam>>> Get()
            => await _context.CustomerTeams.ToListAsync();

        // GET api/CustomerTeamsApi/5
        [RequireLogin(Roles = new[] { UserRole.Admin, UserRole.Reporter })]

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerTeam>> Get(int id)
        {
            var t = await _context.CustomerTeams.FindAsync(id);
            if (t == null) return NotFound();
            return t;
        }

        // POST api/CustomerTeamsApi
        [RequireLogin(Roles = new[] { UserRole.Admin })]

        [HttpPost]
        public async Task<ActionResult<CustomerTeam>> Post(CustomerTeam t)
        {
            _context.CustomerTeams.Add(t);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = t.Id }, t);
        }

        // PUT api/CustomerTeamsApi/5
        [RequireLogin(Roles = new[] { UserRole.Admin })]

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CustomerTeam t)
        {
            if (id != t.Id) return BadRequest();
            _context.Entry(t).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.CustomerTeams.AnyAsync(e => e.Id == id))
                    return NotFound();
                throw;
            }
            return NoContent();
        }

        // DELETE api/CustomerTeamsApi/5
        [RequireLogin(Roles = new[] { UserRole.Admin })]

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var t = await _context.CustomerTeams.FindAsync(id);
            if (t == null) return NotFound();
            _context.CustomerTeams.Remove(t);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
