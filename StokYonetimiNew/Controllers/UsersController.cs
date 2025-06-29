using Microsoft.AspNetCore.Mvc;
using StokYonetimiNew.Filters;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    // Sadece Admin rolündeki kullanıcılar erişebilsin
    [RoleAuthorize(UserRole.Admin)]
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
