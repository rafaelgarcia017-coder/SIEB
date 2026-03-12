using Embarcaciones.AplicacionWeb.Models.Utils;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Puerto;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.ZonaNavegacion;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Embarcaciones.AplicacionWeb.Controllers.Catalogos
{
    public class ZonaNavegacionController : CustomController
    {
        private readonly ICatalogoValorService _catalogoValorService;
        private readonly ICatalogoService _catalogoService;

        public ZonaNavegacionController(ICatalogoValorService catalogoValorService, ICatalogoService catalogoService)
        {
            _catalogoValorService = catalogoValorService;
            _catalogoService = catalogoService;
        }

        private async Task<int> ObtenerIdCatalogo()
        {
            var id = await _catalogoService.ObtenerIdCatalogo("ZNNV");
            return id;
        }
        public async Task <IActionResult> Administrar()
        {
            int idCatalogo = await ObtenerIdCatalogo();
            var obtenerRegistro = await _catalogoValorService.ObtenerTodos(idCatalogo).ToListAsync();

            var model = new AdministrarZonaNavegacionVM
            {
                ListaZonaNavegacion = obtenerRegistro.Select(s => new ZonaNavegacionVM
                {
                    IdCatalogo = s.IdCatalogo,
                    IdZonaNavegacion = s.IdCatalogoValor,
                    ZonaNavegacion = s.Nombre,
                    Descripcion = s.Descripcion,
                    FechaCreacion = s.FechaCreacion.ToString("dd/MM/yyyy"),
                    UsuarioCreacion = s.UsuarioCreacionNavigation.Usuario
                }).ToList()

            };
            return View("Administrar", model);
        }
        public ActionResult NuevoZonaNavegacion()
        {
            var model = new ZonaNavegacionVM();
            model.Accion = AccionesController.Nuevo;
            return View("NuevoZonaNavegacion", model);
        }

        public async Task<IActionResult> EditarZonaNavegacion(int id)
        {
            var tipoIdentificacion = await _catalogoValorService.Obtener(id);
            var model = new ZonaNavegacionVM
            {
                IdCatalogo = tipoIdentificacion.IdCatalogo,
                IdZonaNavegacion = tipoIdentificacion.IdCatalogoValor,
                ZonaNavegacion = tipoIdentificacion.Nombre,
                Descripcion = tipoIdentificacion.Descripcion,
                Accion = AccionesController.Editar
            };
            return View("NuevoZonaNavegacion", model);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarZonaNavegacion(ZonaNavegacionVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    AddAdvertencia(this.ErroresFromModel().Texto);
                    return View("NuevoZonaNavegacion", model);
                }

                if (model.Accion == AccionesController.Nuevo)
                {
                    int idCatalogo = await ObtenerIdCatalogo();
                    var responseVerify = await _catalogoValorService.ValidarCatalogo(idCatalogo, model.ZonaNavegacion);
                    if (responseVerify)
                    {
                        AddAdvertencia("La Zona de Navegacion que intentas registrar ya existe. Revisa la información e intenta nuevamente");
                        return View("NuevoPuerto", model);
                    }
                    var zonaNavegacion = new CatalogoValor
                    {
                        IdCatalogo = idCatalogo,
                        Nombre = model.ZonaNavegacion,
                        Descripcion = model.Descripcion,
                        IdUsuarioCreacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                    };
                    var response = await _catalogoValorService.Agregar(zonaNavegacion);
                }
                else
                {
                    var zonaNavegacion = new CatalogoValor
                    {
                        IdCatalogo = model.IdCatalogo,
                        IdCatalogoValor = model.IdZonaNavegacion,
                        Nombre = model.ZonaNavegacion,
                        Descripcion = model.Descripcion,
                        IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                        FechaModificacion = DateTime.Now,
                    };
                    var response = await _catalogoValorService.Actualizar(zonaNavegacion);
                }

                AddExito(model.Accion == AccionesController.Nuevo
                                                                                ? "Zona de Navegacion registrada satisfactoriamente."
                                                                                : "Zona de Navegacion actualizada satisfactoriamente.");
                return RedirectToAction("Administrar");
            }
            catch (Exception ex)
            {
                AddError("Ha ocurrido un error. Contacte al Administrador.");
                return View("NuevoZonaNavegacion", model);
                throw;
            }
           
        }
        [HttpPost]
        public async Task<ActionResult> EliminarZonaNavegacion(int id)
        {
            var puedeEliminar = await _catalogoValorService.ValidarEliminar(id, "ZNNV");

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
