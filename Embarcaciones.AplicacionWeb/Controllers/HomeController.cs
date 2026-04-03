using Embarcaciones.AplicacionWeb.Models;
using Embarcaciones.AplicacionWeb.Models.ViewModels;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Procesos.Dashboard;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Embarcaciones.AplicacionWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmbarcacionService _embarcacionService;

        public HomeController(IEmbarcacionService embarcacionService)
        {
            _embarcacionService = embarcacionService;
        }
        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            var resultado = await _embarcacionService.ObtenerDashboard();

            var model = new DashboardVM
            {
                // KPIs
                TotalEmbarcaciones = resultado.TotalEmbarcaciones,
                TotalPersonas = resultado.TotalPersonas,
                TotalPuertos = resultado.TotalPuertos,
                TotalNacionalidades = resultado.TotalNacionalidades,

                // Gráficas
                RegistrosMes = resultado.RegistrosMes.Select(r => new DashboardVM.RegistroMesVM
                {
                    Mes = r.Mes,
                    Cantidad = r.Cantidad
                }).ToList(),

                // Gráficas: mapeamos tipos de embarcación
                TiposEmbarcacion = resultado.TiposEmbarcacion.Select(t => new DashboardVM.TipoEmbarcacionVM
                {
                    Tipo = t.Tipo,
                    Cantidad = t.Cantidad
                }).ToList()
            };

            return View("Dashboard", model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
