using Embarcaciones.AplicacionWeb.Models.ViewModels.Seguridad;
using Embarcaciones.BLL.Service;
using Microsoft.AspNetCore.Mvc;

namespace Embarcaciones.AplicacionWeb.Controllers.Seguridad
{
    public class LoginController : Controller
    {
    private readonly ICuentaService _cuentaService;

    public LoginController(ICuentaService cuentaService)
    {
        _cuentaService = cuentaService;
    }
        public IActionResult InicioSesion ()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> InicioSesion(InicioSesionVM sesion)
        {
            if (await _cuentaService.Login(sesion.Cuenta, sesion.Clave))
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "Cuenta o contraseña incorrecta");
            return View(sesion);
        }
    }
}
