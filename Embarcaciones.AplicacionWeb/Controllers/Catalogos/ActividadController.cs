using Embarcaciones.AplicacionWeb.Models.Utils;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Actividad;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Puerto;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Embarcaciones.AplicacionWeb.Controllers.Catalogos
{
    public class ActividadController : CustomController
    {
        private readonly ICatalogoValorService _catalogoValorService;
        private readonly ICatalogoService _catalogoService;

        public ActividadController(ICatalogoValorService catalogoValorService, ICatalogoService catalogoService)
        {
            _catalogoValorService = catalogoValorService;
            _catalogoService = catalogoService;
        }
        private async Task<int> ObtenerIdCatalogo()
        {
            var id = await _catalogoService.ObtenerIdCatalogo("ACTV");
            return id;
        }
        public async Task< IActionResult> Administrar()
        {
            int idCatalogo = await ObtenerIdCatalogo();
            var obtenerRegistro = await _catalogoValorService.ObtenerTodos(idCatalogo).ToListAsync();

            var model = new AdministrarActividadVM
            {
                ListaActividad = obtenerRegistro.Select(s => new ActividadVM
                {
                    IdCatalogo = s.IdCatalogo,
                    IdActividad = s.IdCatalogoValor,
                    Actividad = s.Nombre,
                    Descripcion = s.Descripcion,
                    FechaCreacion = s.FechaCreacion.ToString("dd/MM/yyyy"),
                    UsuarioCreacion = s.UsuarioCreacionNavigation.Usuario
                }).ToList()

            };
            return View("Administrar", model);
        }

        public ActionResult NuevoActividad()
        {
            var model = new ActividadVM();
            model.Accion = AccionesController.Nuevo;
            return View("NuevoActividad", model);
        }

        public async Task<IActionResult> EditarActividad(int id)
        {
            var actividad = await _catalogoValorService.Obtener(id);
            var model = new ActividadVM
            {
                IdCatalogo = actividad.IdCatalogo,
                IdActividad = actividad.IdCatalogoValor,
                Actividad = actividad.Nombre,
                Descripcion = actividad.Descripcion,
                Accion = AccionesController.Editar
            };
            return View("NuevoActividad", model);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarActividad(ActividadVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    AddAdvertencia(this.ErroresFromModel().Texto);
                    return View("NuevoActividad", model);
                }
                int idCatalogo = await ObtenerIdCatalogo();
                int? id =  model.IdActividad > 0 ? model.IdActividad : null;
                var responseVerify = await _catalogoValorService.ValidarCatalogo(id,idCatalogo, model.Actividad);
                if (responseVerify)
                {
                    AddAdvertencia("La Actividad que intentas registrar ya existe. Revisa la información e intenta nuevamente");
                    return View("NuevoActividad", model);
                }
                if (model.Accion == AccionesController.Nuevo)
                {
                   
                    var actividad = new CatalogoValor
                    {
                        IdCatalogo = idCatalogo,
                        Nombre = model.Actividad,
                        Descripcion = model.Descripcion,
                        IdUsuarioCreacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                    };
                    var response = await _catalogoValorService.Agregar(actividad);
                }
                else
                {
                    var actividad = new CatalogoValor
                    {
                        IdCatalogo = model.IdCatalogo,
                        IdCatalogoValor = model.IdActividad ,
                        Nombre = model.Actividad,
                        Descripcion = model.Descripcion,
                        IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                        FechaModificacion = DateTime.Now,
                    };
                    var response = await _catalogoValorService.Actualizar(actividad);
                }

                AddExito(model.Accion == AccionesController.Nuevo
                                                                        ? "Actividad registrada satisfactoriamente."
                                                                        : "Actividad actualizada satisfactoriamente.");
                return RedirectToAction("Administrar");
            }
            catch (Exception ex)
            {
                AddError("Ha ocurrido un error. Contacte al Administrador.");
                 return View("NuevoActividad", model);
                throw;
            }
    
        }
        [HttpPost]
        public async Task<ActionResult> EliminarActividad(int id)
        {
            try
            {
                var puedeEliminar = await _catalogoValorService.ValidarEliminar(id, "ACTV");

                if (!puedeEliminar)
                    return Json(new { success = false, mensaje = "No se puede eliminar este registro porque está asociado a otros datos. Para continuar, primero desvincule o elimine los registros relacionados." });

                var actividad = new CatalogoValor
                {
                    IdCatalogoValor = id,
                    EstaActivo = false,
                    EsHistorico = true,
                    IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    FechaModificacion = DateTime.Now
                };

                await _catalogoValorService.Eliminar(actividad);

                return Json(new { success = true, mensaje = "Se eliminó con éxito." });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, mensaje = "Ha ocurrido un error contacte al Administrador." });
            }
        
        }
    }
}
