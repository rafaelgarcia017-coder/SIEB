using Microsoft.AspNetCore.Mvc;

namespace Embarcaciones.AplicacionWeb.Controllers
{
    public class MunicipioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
