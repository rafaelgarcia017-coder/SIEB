using Embarcaciones.AplicacionWeb.Models.Utils;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Marca;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Embarcaciones.AplicacionWeb.Controllers.Catalogos
{
    public class MarcaController : CustomController
    {
        private readonly ICatalogoValorService _catalogoValorService;
        private readonly ICatalogoService _catalogoService;

        public MarcaController(ICatalogoValorService catalogoValorService, ICatalogoService catalogoService)
        {
            _catalogoValorService = catalogoValorService;
            _catalogoService = catalogoService;
        }

        private async Task<int> ObtenerIdCatalogo()
        {
            var id = await _catalogoService.ObtenerIdCatalogo("MRCA");
            return id;
        }

        public async Task<IActionResult> Administrar()
        {
            int idCatalogo = await ObtenerIdCatalogo();
            var obtenerRegistro = await _catalogoValorService.ObtenerTodos(idCatalogo).ToListAsync();

            var model = new AdministrarMarcaVM
            {
                ListaMarca = obtenerRegistro.Select(s => new MarcaVM
                {
                    IdCatalogo = s.IdCatalogo,
                    IdMarca = s.IdCatalogoValor,
                    Marca = s.Nombre,
                    Descripcion = s.Descripcion,
                    FechaCreacion = s.FechaCreacion.ToString("dd/MM/yyyy"),
                    UsuarioCreacion = s.UsuarioCreacionNavigation.Usuario
                }).ToList()

            };
            return View("Administrar", model);
        }
        public ActionResult NuevoMarca()
        {
            var model = new MarcaVM();
            model.Accion = AccionesController.Nuevo;
            return View("NuevoMarca", model);
        }

        public async Task<IActionResult> EditarMarca(int id)
        {
            var tipoIdentificacion = await _catalogoValorService.Obtener(id);
            var model = new MarcaVM
            {
                IdCatalogo = tipoIdentificacion.IdCatalogo,
                IdMarca = tipoIdentificacion.IdCatalogoValor,
                Marca = tipoIdentificacion.Nombre,
                Descripcion = tipoIdentificacion.Descripcion,
                Accion = AccionesController.Editar
            };
            return View("NuevoMarca", model);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarMarca(MarcaVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    AddAdvertencia(this.ErroresFromModel().Texto);
                    return View("NuevoMarca", model);
                }

                if (model.Accion == AccionesController.Nuevo)
                {
                    int idCatalogo = await ObtenerIdCatalogo();
                    var responseVerify = await _catalogoValorService.ValidarCatalogo(idCatalogo, model.Marca);
                    if (responseVerify)
                    {
                        AddAdvertencia("La Marca que intentas registrar ya existe. Revisa la información e intenta nuevamente");
                        return View("NuevoMarca", model);
                    }
                    var tipoIdentificacion = new CatalogoValor
                    {
                        IdCatalogo = idCatalogo,
                        Nombre = model.Marca,
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
                        IdCatalogoValor = model.IdMarca,
                        Nombre = model.Marca,
                        Descripcion = model.Descripcion,
                        IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                        FechaModificacion = DateTime.Now,
                    };
                    var response = await _catalogoValorService.Actualizar(tipoIdentificacion);
                }

                AddExito(model.Accion == AccionesController.Nuevo
                                                                             ? "Marca registrada satisfactoriamente."
                                                                             : "Marca actualizada satisfactoriamente.");

                return RedirectToAction("Administrar");
            }
            catch (Exception)
            {
                AddError("Ha ocurrido un error. Contacte al Administrador.");
                return View("NuevoMarca", model);
                throw;
            }

        }

        [HttpPost]
        public async Task<ActionResult> EliminarMarca(int id)
        {
            var puedeEliminar = await _catalogoValorService.ValidarEliminar(id, "MRCA");

            if (!puedeEliminar)
                return Json(new { success = false, mensaje = "No se puede eliminar este registro porque está asociado a otros datos. Para continuar, primero desvincule o elimine los registros relacionados." });

            var marca = new CatalogoValor
            {
                IdCatalogoValor = id,
                //EstaActivo = false,
                //EsHistorico = true,
                IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                FechaModificacion = DateTime.Now
            };

            await _catalogoValorService.Eliminar(marca);

            return Json(new { success = true, mensaje = "Se eliminó con éxito." });
        }

    }
}
