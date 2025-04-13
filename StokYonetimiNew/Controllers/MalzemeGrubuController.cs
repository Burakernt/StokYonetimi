using Microsoft.AspNetCore.Mvc;

namespace StokYonetimiNew.Controllers
{
    public class MalzemeGrubuController : Controller
    {
        public ActionResult Index()
        {
            // Views/MaterialGroup klasörü altında MalzemeGrubuTanımlama.cshtml dosyası olmalı.
            return View("MalzemeGrubuTanımlama");
        }
    }
}
