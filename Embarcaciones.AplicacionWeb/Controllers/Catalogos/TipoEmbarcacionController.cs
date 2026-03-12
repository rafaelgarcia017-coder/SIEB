using Embarcaciones.AplicacionWeb.Models.Utils;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Material;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.TipoEmbarcacion;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Embarcaciones.AplicacionWeb.Controllers.Catalogos
{
    public class TipoEmbarcacionController : CustomController
    {
        private readonly ICatalogoValorService _catalogoValorService;
        private readonly ICatalogoService _catalogoService;

        public TipoEmbarcacionController(ICatalogoValorService catalogoValorService, ICatalogoService catalogoService)
        {
            _catalogoValorService = catalogoValorService;
            _catalogoService = catalogoService;
        }

        private async Task<int> ObtenerIdCatalogo()
        {
            var id = await _catalogoService.ObtenerIdCatalogo("TPEM");
            return id;
        }

        public async Task<IActionResult> Administrar()
        {
            int idCatalogo = await ObtenerIdCatalogo();
            var obtenerRegistro = await _catalogoValorService.ObtenerTodos(idCatalogo).ToListAsync();

            var model = new AdministrarTipoEmbarcacionVM
            {
                ListaTipoEmbarcacion = obtenerRegistro.Select(s => new TipoEmbarcacionVM
                {
                    IdCatalogo = s.IdCatalogo,
                    IdTipoEmbarcacion = s.IdCatalogoValor,
                    TipoEmbarcacion = s.Nombre,
                    Descripcion = s.Descripcion,
                    FechaCreacion = s.FechaCreacion.ToString("dd/MM/yyyy"),
                    UsuarioCreacion = s.UsuarioCreacionNavigation.Usuario
                }).ToList()

            };
            return View("Administrar", model);
        }
        public ActionResult NuevoTipoEmbarcacion()
        {
            var model = new TipoEmbarcacionVM();
            model.Accion = AccionesController.Nuevo;
            return View("NuevoTipoEmbarcacion", model);
        }   
        public async Task<IActionResult> EditarTipoEmbarcacion(int id)
        {
            var tipoEmbarcacion = await _catalogoValorService.Obtener(id);
            var model = new TipoEmbarcacionVM
            {
                IdCatalogo = tipoEmbarcacion.IdCatalogo,
                IdTipoEmbarcacion = tipoEmbarcacion.IdCatalogoValor,
                TipoEmbarcacion = tipoEmbarcacion.Nombre,
                Descripcion = tipoEmbarcacion.Descripcion,
                Accion = AccionesController.Editar
            };
            return View("NuevoTipoEmbarcacion", model);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarTipoEmbarcacion(TipoEmbarcacionVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    AddAdvertencia(this.ErroresFromModel().Texto);
                    return View("NuevoTipoEmbarcacion", model);
                }

                if (model.Accion == AccionesController.Nuevo)
                {
                    int idCatalogo = await ObtenerIdCatalogo();
                    var responseVerify = await _catalogoValorService.ValidarCatalogo(idCatalogo, model.TipoEmbarcacion);
                    if (responseVerify)
                    {
                        AddAdvertencia("El Tipo de Embarcacion que intentas registrar ya existe. Revisa la información e intenta nuevamente");
                        return View("NuevoTipoEmbarcacion", model);
                    }
                    var material = new CatalogoValor
                    {
                        IdCatalogo = idCatalogo,
                        Nombre = model.TipoEmbarcacion,
                        Descripcion = model.Descripcion,
                        IdUsuarioCreacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                    };
                    var response = await _catalogoValorService.Agregar(material);
                }
                else
                {
                    var material = new CatalogoValor
                    {
                        IdCatalogo = model.IdCatalogo,
                        IdCatalogoValor = model.IdTipoEmbarcacion,
                        Nombre = model.TipoEmbarcacion,
                        Descripcion = model.Descripcion,
                        IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                        FechaModificacion = DateTime.Now,
                    };
                    var response = await _catalogoValorService.Actualizar(material);
                }
                AddExito(model.Accion == AccionesController.Nuevo
                                                                ? "Tipo Embarcacion registrado satisfactoriamente."
                                                                : "Tipo Embarcacion actualizada satisfactoriamente.");

                return RedirectToAction("Administrar");
            }
            catch (Exception ex)
            {
                AddError("Ha ocurrido un error. Contacte al Administrador.");
                return View("NuevoTipoEmbarcacion", model);
                throw;
            }

        }
        [HttpPost]
        public async Task<ActionResult> eliminarTipoEmbarcacion(int id)
        {
            var puedeEliminar = await _catalogoValorService.ValidarEliminar(id, "TPEM");

            if (!puedeEliminar)
                return Json(new { success = false, mensaje = "No se puede eliminar este registro porque está asociado a otros datos. Para continuar, primero desvincule o elimine los registros relacionados." });

            var marca = new CatalogoValor
            {
                IdCatalogoValor = id,
                EstaActivo = false,
                EsHistorico = true,
                IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                FechaModificacion = DateTime.Now
            };

            await _catalogoValorService.Eliminar(marca);

            return Json(new { success = true, mensaje = "Se eliminó con éxito." });
        }

    }
}
