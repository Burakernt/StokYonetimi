using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    [RoleAuthorize(UserRole.Admin)]
    public class DataEntryController : Controller
    {
        public IActionResult MaterialEntry()
        {
            return View();
        }
        
        public IActionResult MaterialOut()
        {
            return View();
        }
    }
}
