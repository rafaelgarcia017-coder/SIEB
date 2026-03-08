using Embarcaciones.AplicacionWeb.Models.Utils;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Departamento;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Puerto;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.TipoIdentificacion;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Embarcaciones.AplicacionWeb.Controllers.Catalogos
{
    public class PuertoController : CustomController
    {
        private readonly ICatalogoValorService _catalogoValorService;
        private readonly ICatalogoService _catalogoService;

        public PuertoController (ICatalogoValorService catalogoValorService, ICatalogoService catalogoService)
        {
            _catalogoValorService = catalogoValorService;
            _catalogoService = catalogoService;
        }

        private async Task<int> ObtenerIdCatalogo()
        {
            var id = await _catalogoService.ObtenerIdCatalogo("PRTO");
            return id;
        }
        public async Task< IActionResult> Administrar()
        {
            int idCatalogo = await ObtenerIdCatalogo();
            var obtenerRegistro = await _catalogoValorService.ObtenerTodos(idCatalogo).ToListAsync();

            var model = new AdministrarPuertoVM
            {
                ListaPuertos = obtenerRegistro.Select(s => new PuertoVM
                {
                    IdCatalogo = s.IdCatalogo,
                    IdPuerto = s.IdCatalogoValor,
                    Puerto = s.Nombre,
                    Descripcion = s.Descripcion,
                    FechaCreacion = s.FechaCreacion.ToString("dd/MM/yyyy"),
                    UsuarioCreacion = s.UsuarioCreacionNavigation.Usuario
                }).ToList()

            };
            return View("Administrar", model);
         }
        public ActionResult NuevoPuerto()
        {
            var model = new PuertoVM();
            model.Accion = AccionesController.Nuevo;
            return View("NuevoPuerto", model);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarPuerto(PuertoVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    AddAdvertencia(this.ErroresFromModel().Texto);
                    return View("NuevoPuerto", model);
                }

                if (model.Accion == AccionesController.Nuevo)
                {
                    int idCatalogo = await ObtenerIdCatalogo();
                    var responseVerify = await _catalogoValorService.ValidarCatalogo(idCatalogo, model.Puerto);
                    if (responseVerify)
                    {
                        AddAdvertencia("El Puerto que intentas registrar ya existe. Revisa la información e intenta nuevamente");
                        return View("NuevoPuerto", model);
                    }
                    var tipoIdentificacion = new CatalogoValor
                    {
                        IdCatalogo = idCatalogo,
                        Nombre = model.Puerto,
                        Descripcion = model.Descripcion,
                        IdUsuarioCreacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                    };
                    var response = await _catalogoValorService.Agregar(tipoIdentificacion);
                }
                else
                {
                    var tipoIdentificacion = new CatalogoValor
                    {
                        IdCatalogo = model.IdCatalogo,
                        IdCatalogoValor = model.IdPuerto,
                        Nombre = model.Puerto,
                        Descripcion = model.Descripcion,
                        IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                        FechaModificacion = DateTime.Now,
                    };
                    var response = await _catalogoValorService.Actualizar(tipoIdentificacion);
                }

                AddExito(model.Accion == AccionesController.Nuevo
                                                                             ? "Puerto registrado satisfactoriamente."
                                                                             : "Puerto actualizado satisfactoriamente.");

                return RedirectToAction("Administrar");
            }
            catch (Exception)
            {
                AddError("Ha ocurrido un error. Contacte al Administrador.");
                return View("NuevoZonaNavegacion", model);
                throw;
            }
         
        }

        public async Task<IActionResult> EditarPuerto(int id)
        {
            var tipoIdentificacion = await _catalogoValorService.Obtener(id);
            var model = new PuertoVM
            {
                IdCatalogo = tipoIdentificacion.IdCatalogo,
                IdPuerto = tipoIdentificacion.IdCatalogoValor,
                Puerto = tipoIdentificacion.Nombre,
                Descripcion = tipoIdentificacion.Descripcion,
                Accion = AccionesController.Editar
            };
            return View("NuevoPuerto", model);
        }
    }
}
