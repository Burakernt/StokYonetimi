using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Models;
using BCrypt.Net;   // <<< ekleyin
using StokYonetimiNew.Filters;
using Microsoft.AspNetCore.Authorization;

namespace StokYonetimiNew.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly StokContext _db;
        public AccountController(StokContext db) => _db = db;

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password, UserRole role)
        {
            if (await _db.Users.AnyAsync(u => u.Username == username))
                ModelState.AddModelError("", "Bu kullanıcı adı zaten kullanımda.");

            if (!ModelState.IsValid)
                return View();

            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User
            {
                Username = username,
                PasswordHash = hash,
                Role = role
            };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login() => View();

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _db.Users
                .SingleOrDefaultAsync(u => u.Username == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                ModelState.AddModelError("", "Kullanıcı adı veya parola hatalı.");
                return View();
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("UserRole", user.Role.ToString());

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
    }
}
