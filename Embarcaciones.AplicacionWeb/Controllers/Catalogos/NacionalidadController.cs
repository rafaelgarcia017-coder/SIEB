using Embarcaciones.AplicacionWeb.Models.Utils;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Actividad;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Material;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Nacionalidad;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Embarcaciones.AplicacionWeb.Controllers.Catalogos
{
    public class NacionalidadController : CustomController
    {
        private readonly ICatalogoValorService _catalogoValorService;
        private readonly ICatalogoService _catalogoService;

        public NacionalidadController(ICatalogoValorService catalogoValorService, ICatalogoService catalogoService)
        {
            _catalogoValorService = catalogoValorService;
            _catalogoService = catalogoService;
        }
        private async Task<int> ObtenerIdCatalogo()
        {
            var id = await _catalogoService.ObtenerIdCatalogo("NACI");
            return id;
        }

        public async Task<IActionResult> Administrar()
        {
            int idCatalogo = await ObtenerIdCatalogo();
            var obtenerRegistro = await _catalogoValorService.ObtenerTodos(idCatalogo).ToListAsync();

            var model = new AdministrarNacionalidadVM
            {
                ListaNacionalidad= obtenerRegistro.Select(s => new NacionalidadVM
                {
                    IdCatalogo = s.IdCatalogo,
                    IdNacionalidad = s.IdCatalogoValor,
                    Nacionalidad = s.Nombre,
                    Descripcion = s.Descripcion,
                    FechaCreacion = s.FechaCreacion.ToString("dd/MM/yyyy"),
                    UsuarioCreacion = s.UsuarioCreacionNavigation.Usuario
                }).ToList()

            };
            return View("Administrar", model);
        }
        public ActionResult NuevoNacionalidad()
        {
            var model = new NacionalidadVM();
            model.Accion = AccionesController.Nuevo;
            return View("NuevoNacionalidad", model);
        }
        public async Task<IActionResult> EditarNacionalidad(int id)
        {
            var nacionalidad = await _catalogoValorService.Obtener(id);
            var model = new NacionalidadVM
            {
                IdCatalogo = nacionalidad.IdCatalogo,
                IdNacionalidad = nacionalidad.IdCatalogoValor,
                Nacionalidad = nacionalidad.Nombre,
                Descripcion = nacionalidad.Descripcion,
                Accion = AccionesController.Editar
            };
            return View("NuevoMaterial", model);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarNacionalidad(NacionalidadVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    AddAdvertencia(this.ErroresFromModel().Texto);
                    return View("NuevoNacionalidad", model);
                }

                if (model.Accion == AccionesController.Nuevo)
                {
                    int idCatalogo = await ObtenerIdCatalogo();
                    var responseVerify = await _catalogoValorService.ValidarCatalogo(idCatalogo, model.Nacionalidad);
                    if (responseVerify)
                    {
                        AddAdvertencia("La Nacionalidad que intentas registrar ya existe. Revisa la información e intenta nuevamente");
                        return View("NuevoNacionalidad", model);
                    }
                    var material = new CatalogoValor
                    {
                        IdCatalogo = idCatalogo,
                        Nombre = model.Nacionalidad,
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
                        IdCatalogoValor = model.IdNacionalidad,
                        Nombre = model.Nacionalidad,
                        Descripcion = model.Descripcion,
                        IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                        FechaModificacion = DateTime.Now,
                    };
                    var response = await _catalogoValorService.Actualizar(material);
                }
                AddExito(model.Accion == AccionesController.Nuevo
                                                                ? "Nacionalidad registrada satisfactoriamente."
                                                                : "Nacionalidad actualizada satisfactoriamente.");

                return RedirectToAction("Administrar");
            }
            catch (Exception ex)
            {
                AddError("Ha ocurrido un error. Contacte al Administrador.");
                return View("NuevoNacionalidad", model);
                throw;
            }

        }

        [HttpPost]
        public async Task<ActionResult> EliminarNacionalidad(int id)
        {
            var puedeEliminar = await _catalogoValorService.ValidarEliminar(id, "NACI");

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
