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

            viewModel.Propietario.ListaDepartamentos = new List<SelectListItem>();
            viewModel.Propietario.ListaMunicipio = new List<SelectListItem>();
            viewModel.Propietario.ListaNacionalidad = new List<SelectListItem>();
            viewModel.Propietario.ListaTipoIdentificacion = new List<SelectListItem>();

            return viewModel;
        }
    }
}
