// Controllers/MaterialExitsController.cs
using Microsoft.AspNetCore.Mvc;
using StokYonetimiNew.Filters;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    [RoleAuthorize(UserRole.Admin)]
    public class MaterialExitsController : Controller
    {
        public IActionResult Index()
        {
            // Vue.js frontend veriyi API’dan çekecek
            return View();
        }
    }
}
