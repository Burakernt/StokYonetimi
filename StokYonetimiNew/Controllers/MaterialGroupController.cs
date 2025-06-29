using Microsoft.AspNetCore.Mvc;
using StokYonetimiNew.Filters;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    [RoleAuthorize(UserRole.Admin)]
    public class MaterialGroupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
