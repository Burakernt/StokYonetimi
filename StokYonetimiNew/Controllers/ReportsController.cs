using Microsoft.AspNetCore.Mvc;
using StokYonetimiNew.Filters;
using StokYonetimiNew.Models;

namespace YourNamespace.Controllers
{
    [RequireLogin(Roles = new[] { UserRole.Admin, UserRole.Reporter })]

    public class ReportsController : Controller
    {
        public IActionResult CategoryHierarchy()
        {
            return View();
        }
        
        public IActionResult MaterialEntryData()
        {
            return View();
        }
        
        public IActionResult MaterialListData()
        {
            return View();
        }
        
        public IActionResult MaterialOutData()
        {
            return View();
        }
    }
}
