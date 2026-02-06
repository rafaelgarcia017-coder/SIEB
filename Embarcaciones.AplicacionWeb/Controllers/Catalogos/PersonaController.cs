using Embarcaciones.AplicacionWeb.Models.Utils;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Departamento;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Persona;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace Embarcaciones.AplicacionWeb.Controllers.Catalogos
{
    public class PersonaController : CustomController
    {

        private readonly IPersonaService _personaService;
        private readonly ICatalogoService _catalogoService;
        private readonly ICatalogoValorService _catalogoValorService;

        public int idUsuario => Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        public PersonaController(IPersonaService personaService, ICatalogoService catalogoService, ICatalogoValorService catalogoValorService)
        {
            _personaService = personaService;
            _catalogoService = catalogoService;
            _catalogoValorService = catalogoValorService;
        }

        private async Task<int> ObtenerIdCatalogo()
        {
            var id = await _catalogoService.ObtenerIdCatalogo("TIDF");
            return id;
        }

        public async Task<IActionResult> Administrar()
        {
            var obtenerRegistros = await _personaService.ObtenerTodos().ToListAsync();
            var model = new AdministrarPersonasVM
            {
                ListaPersonas = obtenerRegistros
                   .Select(s => new PersonaVM
                   {
                       IdPersona = s.IdPersona,
                       NombreCompleto = s.NombreCompleto,                     
                       TipoIdentificacion = s.TipoIdentificacionNavigation?.Nombre ??"" ,
                       Identificacion = s.Identificacion,
                       Direccion = s.Direccion,
                       Telefono = s.Telefono,
                       Correo = s.Correo,
                       FechaCreacion = s.FechaCreacion.ToString("dd/MM/yyyy"),                  
                       UsuarioCreacion = s.UsuarioCreacionNavigation?.Usuario ?? ""

                   }).ToList()
                // materializa la proyección directamente
            };

            return View("Administrar", model);
        }

        public async Task<PersonaVM> LlenarModelo(PersonaVM viewModel = null)
        {
            viewModel = viewModel ?? new PersonaVM();
            int  idCatalogo  =  await ObtenerIdCatalogo();
            var tipoIdentificaciones = _catalogoValorService.ObtenerTodos(idCatalogo).ToList();
            viewModel.ListaTipoIdentificacion = tipoIdentificaciones.Select(x => new SelectListItem { 
              Value = x.IdCatalogoValor.ToString(),
              Text  = x.Nombre            
            }).ToList();

            return viewModel;
        }
        public async Task< IActionResult> NuevaPersona()
        {
            var model = await LlenarModelo();
            model.Accion = AccionesController.Nuevo;
            return View("NuevaPersona", model);
        }
        [HttpPost]
        public async Task< IActionResult> GuardarPersona(PersonaVM model)
        {            
            if (!ModelState.IsValid) {
                AddAdvertencia(this.ErroresFromModel().Texto);
                return View("NuevaPersona",await LlenarModelo(model));
            }
            if (model.Accion == AccionesController.Nuevo)
            {
                var responseVerify = await _personaService.ValidarDuplicados(model.NombreCompleto, model.Identificacion,model.IdTipoIdentificacion ?? 0);
                if (responseVerify)
                {
                    AddAdvertencia("La Persona que intentas registrar ya existe. Revisa la información e intenta nuevamente");
                    return View("NuevaPersona", LlenarModelo(model));
                }
                var persona = new Persona
                {
                    NombreCompleto = model.NombreCompleto,
                    IdTipoIdentificacion = model.IdTipoIdentificacion,
                    Identificacion = model.Identificacion,
                    Direccion = model.Direccion,
                    Telefono = model.Telefono,
                    Correo = model.Correo,
                    IdUsuarioCreacion = idUsuario
                };

                bool response = await _personaService.Agregar(persona);
            }
            else {
                var persona = new Persona
                {
                    NombreCompleto = model.NombreCompleto,
                    IdTipoIdentificacion = model.IdTipoIdentificacion,
                    Identificacion = model.Identificacion,
                    Direccion = model.Direccion,
                    Telefono = model.Telefono,
                    Correo = model.Correo,
                    IdUsuarioModificacion = idUsuario,
                    FechaModificacion =DateTime.Now
                };

                bool response = await _personaService.Actualizar(persona);
            }
            
             return RedirectToAction("Administrar");
        }

    }
}
