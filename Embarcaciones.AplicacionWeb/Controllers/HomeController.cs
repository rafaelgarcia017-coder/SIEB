using Embarcaciones.AplicacionWeb.Models;
using Embarcaciones.AplicacionWeb.Models.ViewModels;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Embarcaciones.AplicacionWeb.Controllers
{
    public class HomeController : Controller
    {
     
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
