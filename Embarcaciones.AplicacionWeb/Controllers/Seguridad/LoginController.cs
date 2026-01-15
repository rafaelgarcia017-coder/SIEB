using Embarcaciones.AplicacionWeb.Models.ViewModels.Seguridad;
using Microsoft.AspNetCore.Mvc;

namespace Embarcaciones.AplicacionWeb.Controllers.Seguridad
{
    public class LoginController : Controller
    {
        public IActionResult InicioSesion ()
        {
            return View();
        }
        //public IActionResult InicioSesion(InicioSesionVM sesion)
        //{
        //    return View();
        //}
    }
}
