using Embarcaciones.AplicacionWeb.Models.Utils;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Color;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Puerto;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Embarcaciones.AplicacionWeb.Controllers.Catalogos
{
    public class ColorController : CustomController
    {
        private readonly ICatalogoValorService _catalogoValorService;
        private readonly ICatalogoService _catalogoService;

        public ColorController(ICatalogoValorService catalogoValorService, ICatalogoService catalogoService)
        {
            _catalogoValorService = catalogoValorService;
            _catalogoService = catalogoService;
        }

        private async Task<int> ObtenerIdCatalogo()
        {
            var id = await _catalogoService.ObtenerIdCatalogo("COLR");
            return id;
        }
        public async Task<IActionResult> Administrar()
        {
            int idCatalogo = await ObtenerIdCatalogo();
            var obtenerRegistro = await _catalogoValorService.ObtenerTodos(idCatalogo).ToListAsync();

            var model = new AdministrarColorVM
            {
                ListaColor = obtenerRegistro.Select(s => new ColorVM
                {
                    IdCatalogo = s.IdCatalogo,
                    IdColor = s.IdCatalogoValor,
                    Color = s.Nombre,
                    Descripcion = s.Descripcion,
                    FechaCreacion = s.FechaCreacion?.ToString("dd/MM/yyyy"),
                    UsuarioCreacion = s.UsuarioCreacionNavigation.Usuario
                }).ToList()

            };
            return View("Administrar", model);
        }
        public ActionResult NuevoColor()
        {
            var model = new ColorVM();
            model.Accion = AccionesController.Nuevo;
            return View("NuevoColor", model);
        }

        public async Task<IActionResult> EditarColor(int id)
        {
            var color = await _catalogoValorService.Obtener(id);
            var model = new ColorVM
            {
                IdCatalogo = color.IdCatalogo,
                IdColor = color.IdCatalogoValor,
                Color = color.Nombre,
                Descripcion = color.Descripcion,
                Accion = AccionesController.Editar
            };
            return View("NuevoColor", model);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarColor(ColorVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    AddAdvertencia(this.ErroresFromModel().Texto);
                    return View("NuevoColor", model);
                }

                if (model.Accion == AccionesController.Nuevo)
                {
                    int idCatalogo = await ObtenerIdCatalogo();
                    var responseVerify = await _catalogoValorService.ValidarCatalogo(idCatalogo, model.Color);
                    if (responseVerify)
                    {
                        AddAdvertencia("El Color que intentas registrar ya existe. Revisa la información e intenta nuevamente");
                        return View("NuevoColor", model);
                    }
                    var color = new CatalogoValor
                    {
                        IdCatalogo = idCatalogo,
                        Nombre = model.Color,
                        Descripcion = model.Descripcion,
                        IdUsuarioCreacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                    };
                    var response = await _catalogoValorService.Agregar(color);
                }
                else
                {
                    var color = new CatalogoValor
                    {
                        IdCatalogo = model.IdCatalogo,
                        IdCatalogoValor = model.IdColor,
                        Nombre = model.Color,
                        Descripcion = model.Descripcion,
                        IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                        FechaModificacion = DateTime.Now,
                    };
                    var response = await _catalogoValorService.Actualizar(color);
                }

                AddExito(model.Accion == AccionesController.Nuevo
            ? "Color registrado satisfactoriamente."
            : "Color actualizado satisfactoriamente.");
                return RedirectToAction("Administrar");
            }
            catch (Exception ex)
            {
                AddError("Ha ocurrido un error. Contacte al Administrador.");
                return View("NuevoColor", model);
                throw;
            }
  
        }

        [HttpPost]
        public async Task<ActionResult> EliminarColor(int id)
        {
            var puedeEliminar = await _catalogoValorService.ValidarEliminar(id, "COLR");

            if (!puedeEliminar)
                return Json(new { success = false, mensaje = "No se puede eliminar este registro porque está asociado a otros datos. Para continuar, primero desvincule o elimine los registros relacionados." });

            var color = new CatalogoValor
            {
                IdCatalogoValor = id,
                //EstaActivo = false,
                //EsHistorico = true,
                IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                FechaModificacion = DateTime.Now
            };

            await _catalogoValorService.Eliminar(color);

            return Json(new { success = true, mensaje = "Se eliminó con éxito." });
        }
    }
}
