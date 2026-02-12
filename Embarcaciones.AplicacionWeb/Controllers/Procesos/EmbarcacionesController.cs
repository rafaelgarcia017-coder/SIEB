using Embarcaciones.AplicacionWeb.Models.ViewModels.Procesos.Embarcaciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Embarcaciones.AplicacionWeb.Controllers.Procesos
{
    public class EmbarcacionesController : CustomController
    {
        public IActionResult Administrar()
        {
            var model = new AdministrarEmbarcacionesVM {
              ListaEmbarcaciones = new List<EmbarcacionItem>()
            };

            return View("Administrar", model);
        }
        public IActionResult NuevaEmbarcacion()
        {
            var model = LlenarModelo ( new EmbarcacionesVM ());
            return  View("NuevaEmbarcacion",model);
        }

        public EmbarcacionesVM LlenarModelo(EmbarcacionesVM viewModel)
        {
            viewModel  ??= new EmbarcacionesVM();

            viewModel.EmbarcacionPropietario.ListaDepartamentos = new List<SelectListItem>();
            viewModel.EmbarcacionPropietario.ListaMunicipio = new List<SelectListItem>();
            viewModel.EmbarcacionPropietario.ListaNacionalidad = new List<SelectListItem>();
            viewModel.EmbarcacionPropietario.ListaTipoIdentificacion = new List<SelectListItem>();

            return viewModel;
        }
    }
}
