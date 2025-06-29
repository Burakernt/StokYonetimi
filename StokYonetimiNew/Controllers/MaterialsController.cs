using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Filters;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    [RequireLogin(Roles = new[] { UserRole.Admin })]
    public class MaterialsController : Controller
    {
        private readonly StokContext _context;
        public MaterialsController(StokContext context) => _context = context;

        public async Task<IActionResult> Index()
            => View();
    }
}
