using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Models;
using StokYonetimiNew.Filters;

namespace StokYonetimiNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // API’ye de sadece Admin erişebilsin
    public class UsersApiController : ControllerBase
    {
        private readonly StokContext _db;
        public UsersApiController(StokContext db) => _db = db;
        [RequireLogin(Roles = new[] { UserRole.Admin })]

        // DTO
        public class UserDto
        {
            public string Username { get; set; }
            public string Password { get; set; }   // boş bırakılırsa parola değişmez
            public UserRole Role { get; set; }
        }
        [RequireLogin(Roles = new[] { UserRole.Admin, UserRole.Reporter })]

        // GET /api/UsersApi
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _db.Users
                .Select(u => new { u.Id, u.Username, u.Role })
                .ToListAsync();
            return Ok(list);
        }
        [RequireLogin(Roles = new[] { UserRole.Admin })]

        // POST /api/UsersApi
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto dto)
        {
            if (await _db.Users.AnyAsync(u => u.Username == dto.Username))
                return BadRequest("Bu kullanıcı adı zaten alınmış.");

            var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            _db.Users.Add(new User
            {
                Username = dto.Username,
                PasswordHash = hash,
                Role = dto.Role
            });
            await _db.SaveChangesAsync();
            return Ok();
        }
        [RequireLogin(Roles = new[] { UserRole.Admin })]

        // PUT /api/UsersApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDto dto)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.Username = dto.Username;
            user.Role = dto.Role;
            if (!string.IsNullOrEmpty(dto.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _db.SaveChangesAsync();
            return Ok();
        }
        [RequireLogin(Roles = new[] { UserRole.Admin })]

        // DELETE /api/UsersApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
