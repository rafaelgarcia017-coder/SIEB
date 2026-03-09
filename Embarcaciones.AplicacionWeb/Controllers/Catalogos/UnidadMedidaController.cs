using Embarcaciones.AplicacionWeb.Models.Utils;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.UnidadMedida;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Embarcaciones.AplicacionWeb.Controllers.Catalogos
{
    public class UnidadMedidaController : CustomController
    {
        private readonly IUnidadMedidaService _unidadMedidaService;

        public UnidadMedidaController(IUnidadMedidaService unidadMedidaService)
        {
            _unidadMedidaService = unidadMedidaService;
        }
        public int idUsuario => Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        public async Task<IActionResult> Administrar()
        {
            var obtenerRegistros = await _unidadMedidaService.ObtenerTodos().ToListAsync();
            var model = new AdministrarUnidadMedidaVM
            {
                ListaUnidadMedida = obtenerRegistros
                   .Select(s => new UnidadMedidaVM
                   {
                       IdUnidadMedida = s.IdUnidadMedida,
                       UnidadMedida = s.Nombre,
                       Abreviatura = s.Abreviatura ?? "",
                       FechaCreacion = s.FechaCreacion.ToString("dd/MM/yyyy"),
                       UsuarioCreacion = s.UsuarioCreacionNavigation?.Usuario ?? ""

                   }).ToList()
                // materializa la proyección directamente
            };

            return View("Administrar", model);
        }
        public IActionResult NuevoUnidadMedida()
        {
            var model = new UnidadMedidaVM();
            model.Accion = AccionesController.Nuevo;
            return View("NuevoUnidadMedida", model);
        }
        public async Task<IActionResult> EditarUnidadMedida(int id)
        {
            var unidadMedida = await _unidadMedidaService.Obtener(id);

            var model = new UnidadMedidaVM
            {
                IdUnidadMedida = unidadMedida.IdUnidadMedida,
                UnidadMedida = unidadMedida.Nombre,
                Abreviatura = unidadMedida.Abreviatura,
                Descripcion = unidadMedida.Descripcion,
                Accion = AccionesController.Editar
            };
            return View("NuevaUnidadMedida", model);
        }
        [HttpPost]
        public async Task<ActionResult> EliminarUnidadMedida(int id)
        {
            try
            {
                var unidadMedida = new UnidadMedida
                {
                    IdUnidadMedida = id,
                    EstaActivo = false,
                    EsHistorico = true,
                    IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    FechaModificacion = DateTime.Now
                };
                var responseVerify = await _unidadMedidaService.Eliminar(unidadMedida);
                if (responseVerify)
                    return Json(new { success = true, mensaje = "Se eliminó con éxito." });
                else
                    return Json(new { success = false, mensaje = "Ha ocurrido un error. Contacte al Administrador." });

            }
            catch (Exception)
            {
                return Json(new { success = false, mensaje = "Ha ocurrido un error. Contacte al Administrador." });
                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> GuardarUnidadMedida(UnidadMedidaVM model)
        {
            try
            {
                bool response;
                if (!ModelState.IsValid)
                {
                    AddAdvertencia(this.ErroresFromModel().Texto);
                    return View("NuevaUnidadMedida", model);
                }
                if (model.Accion == AccionesController.Nuevo)
                {
                    var responseVerify = await _unidadMedidaService.ValidarUnidadDuplicadas(model.UnidadMedida, model.Abreviatura, null);
                    if (responseVerify)
                    {
                        AddAdvertencia("La Unidad de Medida que intentas registrar ya existe. Revisa la información e intenta nuevamente");
                        return View("NuevaUnidadMedida", model);
                    }

                    var unidadMedida = new UnidadMedida
                    {
                        Nombre = model.UnidadMedida,
                        Abreviatura = model.Abreviatura,
                        Descripcion = model.Descripcion,
                        IdUsuarioCreacion = idUsuario
                    };

                    response = await _unidadMedidaService.Agregar(unidadMedida);
                }
                else
                {
                    var responseVerify = await _unidadMedidaService.ValidarUnidadDuplicadas(model.UnidadMedida, model.Abreviatura, model.IdUnidadMedida);
                    if (responseVerify)
                    {
                        AddAdvertencia("La Unidad de Medida que intentas registrar ya existe. Revisa la información e intenta nuevamente");
                        return View("NuevaUnidadMedida", model);
                    }

                    var unidadMedida = new UnidadMedida
                    {
                        Nombre = model.UnidadMedida,
                        Abreviatura = model.Abreviatura,
                        Descripcion = model.Descripcion,
                        IdUsuarioModificacion = idUsuario,
                        FechaModificacion = DateTime.Now
                    };

                    response = await _unidadMedidaService.Actualizar(unidadMedida);
                }

                if (response)
                {
                    AddExito(model.Accion == AccionesController.Nuevo
                                                                  ? "Unidad Medida registrada satisfactoriamente."
                                                                  : "Unidad Medida actualizada satisfactoriamente.");
                }
                else
                {
                    AddError("Ha ocurrido un error. Contacte al Administrador.");
                    return View("NuevaUnidadMedida", model);                    
                }
                return RedirectToAction("Administrar");
            }
            catch (Exception)
            {
                AddError("Ha ocurrido un error. Contacte al Administrador.");
                return View("NuevaUnidadMedida", model);
                throw;
            }


        }
    }
}
