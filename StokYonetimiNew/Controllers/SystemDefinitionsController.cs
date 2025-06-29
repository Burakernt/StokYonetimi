using Microsoft.AspNetCore.Mvc;
using StokYonetimiNew.Filters;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Controllers
{
    // Sadece Admin görebilsin
    [RoleAuthorize(UserRole.Admin)]
    public class SystemDefinitionsController : Controller
    {
        public IActionResult MeasurementUnits()
            => View();
    }
}
