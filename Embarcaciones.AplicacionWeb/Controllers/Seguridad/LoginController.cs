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
            var esValido = await _cuentaService.Login(sesion.Cuenta, sesion.Clave);
            if (!esValido)
            {
                ModelState.AddModelError("", "Cuenta o contraseña incorrecta");
                return View(sesion);
            }

            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, sesion.Cuenta)
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
