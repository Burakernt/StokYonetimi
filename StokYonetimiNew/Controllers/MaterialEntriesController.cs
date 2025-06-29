using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Filters;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    [RoleAuthorize(UserRole.Admin)]
    public class MaterialEntriesController : Controller
    {
        private readonly StokContext _context;
        public MaterialEntriesController(StokContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
