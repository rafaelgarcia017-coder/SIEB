using Embarcaciones.AplicacionWeb.Models.ViewModels.Seguridad;
using Embarcaciones.BLL.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Embarcaciones.AplicacionWeb.Controllers.Seguridad
{
    public class LoginController : Controller
    {
        private readonly ICuentaService _cuentaService;

        public LoginController(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }
        public IActionResult InicioSesion()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InicioSesion(InicioSesionVM sesion)
        {
            if (!ModelState.IsValid)
            {
                return View(sesion);
            }

            var cuenta = await _cuentaService.Login(sesion.Cuenta, sesion.Clave);

            if (cuenta == null || cuenta.IdCuenta == 0)
            {
                ModelState.AddModelError(string.Empty, "Cuenta o contraseña incorrecta");
                return View(sesion);
            }

            var claims = new List<Claim>
          {
               new Claim(ClaimTypes.Name, sesion.Cuenta),
               new Claim(ClaimTypes.NameIdentifier, cuenta.IdCuenta.ToString())

           };

            var identity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("InicioSesion", "Login");
        }
    }
}
