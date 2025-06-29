using Microsoft.AspNetCore.Mvc;
using StokYonetimiNew.Data;
using StokYonetimiNew.Filters;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    [RoleAuthorize(UserRole.Admin)]

    public class SuppliersController : Controller
    {
        private readonly StokContext _context;
        public SuppliersController(StokContext context) => _context = context;

        public IActionResult Index()
        {
            return View();
        }
    }
}
