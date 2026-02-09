using Embarcaciones.AplicacionWeb.Models.Utils;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Puerto;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.TipoPropulsion;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Embarcaciones.AplicacionWeb.Controllers.Catalogos
{
    public class TipoPropulsionController : CustomController
    {
        private readonly ICatalogoValorService _catalogoValorService;
        private readonly ICatalogoService _catalogoService;

        public TipoPropulsionController(ICatalogoValorService catalogoValorService, ICatalogoService catalogoService)
        {
            _catalogoValorService = catalogoValorService;
            _catalogoService = catalogoService;
        }

        private async Task<int> ObtenerIdCatalogo()
        {
            var id = await _catalogoService.ObtenerIdCatalogo("TPPR");
            return id;
        }

        public async Task<IActionResult> Administrar()
        {
            int idCatalogo = await ObtenerIdCatalogo();
            var obtenerRegistro = await _catalogoValorService.ObtenerTodos(idCatalogo).ToListAsync();

            var model = new AdministrarTipoPropulsionVM
            {
                ListaTipoPropulsion = obtenerRegistro.Select(s => new TipoPropulsionVM
                {
                    IdCatalogo = s.IdCatalogo,
                    IdTipoPropulsion = s.IdCatalogoValor,
                    ValorTipoPropulsion = s.Nombre,
                    Descripcion = s.Descripcion,
                    FechaCreacion = s.FechaCreacion?.ToString("dd/MM/yyyy"),
                    UsuarioCreacion = s.UsuarioCreacionNavigation.Usuario
                }).ToList()

            };
            return View("Administrar", model);
        }
        public ActionResult NuevoTipoPropulsion()
        {
            var model = new TipoPropulsionVM();
            model.Accion = AccionesController.Nuevo;
            return View("NuevoTipoPropulsion", model);
        }

        public async Task<IActionResult> EditarTipoPropulsion(int id)
        {
            var tipoPropulsion = await _catalogoValorService.Obtener(id);
            var model = new TipoPropulsionVM
            {
                IdCatalogo = tipoPropulsion.IdCatalogo,
                IdTipoPropulsion = tipoPropulsion.IdCatalogoValor,
                ValorTipoPropulsion = tipoPropulsion.Nombre,
                Descripcion = tipoPropulsion.Descripcion,
                Accion = AccionesController.Editar
            };
            return View("NuevoTipoPropulsion", model);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarTipoPropulsion(TipoPropulsionVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    AddAdvertencia(this.ErroresFromModel().Texto);
                    return View("NuevoTipoPropulsion", model);
                }

                if (model.Accion == AccionesController.Nuevo)
                {
                    int idCatalogo = await ObtenerIdCatalogo();
                    var responseVerify = await _catalogoValorService.ValidarCatalogo(idCatalogo, model.ValorTipoPropulsion);
                    if (responseVerify)
                    {
                        AddAdvertencia("El Tipo de Propulsion que intentas registrar ya existe. Revisa la información e intenta nuevamente");
                        return View("NuevoTipoPropulsion", model);
                    }
                    var tipoIdentificacion = new CatalogoValor
                    {
                        IdCatalogo = idCatalogo,
                        Nombre = model.ValorTipoPropulsion,
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
                        IdCatalogoValor = model.IdTipoPropulsion,
                        Nombre = model.ValorTipoPropulsion,
                        Descripcion = model.Descripcion,
                        IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                        FechaModificacion = DateTime.Now,
                    };
                    var response = await _catalogoValorService.Actualizar(tipoIdentificacion);
                }

                AddExito(model.Accion == AccionesController.Nuevo
                                                                             ? "Tipo Propulsion registrada satisfactoriamente."
                                                                             : "Tipo Propulsion actualizada satisfactoriamente.");

                return RedirectToAction("Administrar");
            }
            catch (Exception)
            {
                AddError("Ha ocurrido un error. Contacte al Administrador.");
                return View("NuevoTipoPropulsion", model);
                throw;
            }

        }
    }
}
