// Controllers/MaterialExitsController.cs
using Microsoft.AspNetCore.Mvc;

namespace StokYonetimiNew.Controllers
{
    public class MaterialExitsController : Controller
    {
        public IActionResult Index()
        {
            // Vue.js frontend veriyi API’dan çekecek
            return View();
        }
    }
}
