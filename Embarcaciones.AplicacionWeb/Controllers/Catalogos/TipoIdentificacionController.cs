using Embarcaciones.AplicacionWeb.Models.Utils;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Departamento;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Municipio;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.TipoIdentificacion;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Embarcaciones.AplicacionWeb.Controllers.Catalogos
{
    public class TipoIdentificacionController : CustomController
    {
        private readonly ICatalogoValorService _catalogoValorService;
        private readonly ICatalogoService _catalogoService;
        public TipoIdentificacionController(ICatalogoValorService catalogoValorService, ICatalogoService catalogoService)
        {
            _catalogoValorService = catalogoValorService;
            _catalogoService = catalogoService;
        }

        private async Task<int> ObtenerIdCatalogo()
        {
            var id = await _catalogoService.ObtenerIdCatalogo("TIDF");
            return id;
        }

        public async Task<AdministraTipoIdentificacionVM> LlenarAdministrar()
        {
            int idCatalogo = await ObtenerIdCatalogo();
            var obtenerRegistro = await _catalogoValorService.ObtenerTodos(idCatalogo).ToListAsync();

            var model = new AdministraTipoIdentificacionVM
            {
                ListaTipoIdentificacion = obtenerRegistro.Select(s => new TipoIdentificacionVM
                {
                    IdCatalogo = s.IdCatalogo,
                    IdTipoIdentificacion = s.IdCatalogoValor,
                    ValorTipoIdentificacion = s.Nombre,
                    Descripcion = s.Descripcion,
                    FechaCreacion = s.FechaCreacion.ToString("dd/MM/yyyy"),
                    UsuarioCreacion = s.UsuarioCreacionNavigation.Usuario
                }).ToList()

            };
            return model;
        }

        public async Task<IActionResult> Administrar()
        {
            var modelo = await LlenarAdministrar();
            return View("Administrar", modelo);
        }
        [HttpPost]
        public async Task<ActionResult> EliminarTipoIdentificacion(int id)
        {
            var puedeEliminar = await _catalogoValorService.ValidarEliminar(id, "TIDF");

            if (!puedeEliminar)
                return Json(new { success = false, mensaje = "No se puede eliminar este registro porque está asociado a otros datos. Para continuar, primero desvincule o elimine los registros relacionados." });

            var tipoIdentificacion = new CatalogoValor
            {
                IdCatalogoValor = id,
                EstaActivo = false,
                EsHistorico = true,
                IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                FechaModificacion = DateTime.Now
            };

            await _catalogoValorService.Eliminar(tipoIdentificacion);

            return Json(new { success = true, mensaje = "Se eliminó con éxito." });
        }

        public ActionResult NuevoTipoIdentificacion()
        {
            var model = new TipoIdentificacionVM();
            model.Accion = AccionesController.Nuevo;
            return View("NuevoTipoIdentificacion", model);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarTipoIdentificacion(TipoIdentificacionVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    AddAdvertencia(this.ErroresFromModel().Texto);
                    return View("NuevoTipoIdentificacion", model);
                }
                int? id = model.IdTipoIdentificacion > 0 ? model.IdTipoIdentificacion : null;
                int idCatalogo = await ObtenerIdCatalogo();
                var responseVerify = await _catalogoValorService.ValidarCatalogo(id, idCatalogo, model.ValorTipoIdentificacion);
                if (responseVerify)
                {
                    AddAdvertencia("El Tipo de Indentificacion que intentas registrar ya existe. Revisa la información e intenta nuevamente");
                    return View("NuevoTipoIdentificacion", model);
                }

                if (model.Accion == AccionesController.Nuevo)
                {
               
                    var tipoIdentificacion = new CatalogoValor
                    {
                        IdCatalogo = idCatalogo,
                        Nombre = model.ValorTipoIdentificacion,
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
                        IdCatalogoValor = model.IdTipoIdentificacion,
                        Nombre = model.ValorTipoIdentificacion,
                        Descripcion = model.Descripcion,
                        IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                        FechaModificacion = DateTime.Now,
                    };
                    var response = await _catalogoValorService.Actualizar(tipoIdentificacion);
                }

                AddExito(model.Accion == AccionesController.Nuevo
                                                                ? "Tipo de Identificacion registrada satisfactoriamente."
                                                                : "Tipo de Identificacion actualizada satisfactoriamente.");

                return RedirectToAction("Administrar");
            }
            catch (Exception)
            {
                AddError("Ha ocurrido un error. Contacte al Administrador.");
                return View("NuevoZonaNavegacion", model);
                throw;
            }

        }

        public async Task<IActionResult> EditarTipoIdentificacion(int id)
        {
            var tipoIdentificacion = await _catalogoValorService.Obtener(id);
            var model = new TipoIdentificacionVM
            {
                IdCatalogo = tipoIdentificacion.IdCatalogo,
                IdTipoIdentificacion = tipoIdentificacion.IdCatalogoValor,
                ValorTipoIdentificacion = tipoIdentificacion.Nombre,
                Descripcion = tipoIdentificacion.Descripcion,
                Accion = AccionesController.Editar
            };
            return View("NuevoTipoIdentificacion", model);
        }

    }

}
