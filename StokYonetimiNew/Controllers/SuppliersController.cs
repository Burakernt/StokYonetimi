using Microsoft.AspNetCore.Mvc;
using StokYonetimiNew.Data;

namespace StokYonetimiNew.Controllers
{
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
