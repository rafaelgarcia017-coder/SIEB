using Embarcaciones.AplicacionWeb.Models.Utils;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.SistemaNavegacion;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.TipoPropulsion;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Embarcaciones.AplicacionWeb.Controllers.Catalogos
{
    public class SistemaNavegacionController : CustomController
    {
        private readonly ICatalogoValorService _catalogoValorService;
        private readonly ICatalogoService _catalogoService;

        public SistemaNavegacionController(ICatalogoValorService catalogoValorService, ICatalogoService catalogoService)
        {
            _catalogoValorService = catalogoValorService;
            _catalogoService = catalogoService;
        }

        private async Task<int> ObtenerIdCatalogo()
        {
            var id = await _catalogoService.ObtenerIdCatalogo("STNV");
            return id;
        }
        public async Task<IActionResult> Administrar()
        {
            int idCatalogo = await ObtenerIdCatalogo();
            var obtenerRegistro = await _catalogoValorService.ObtenerTodos(idCatalogo).ToListAsync();

            var model = new AdministrarSistemaNavegacionVM
            {
                ListaSistemaNavegacion = obtenerRegistro.Select(s => new SistemaNavegacionVM
                {
                    IdCatalogo = s.IdCatalogo,
                    IdSistemaNavegacion = s.IdCatalogoValor,
                    SistemaNavegacion = s.Nombre,
                    Descripcion = s.Descripcion,
                    FechaCreacion = s.FechaCreacion.ToString("dd/MM/yyyy"),
                    UsuarioCreacion = s.UsuarioCreacionNavigation.Usuario
                }).ToList()

            };
            return View("Administrar", model);
        }
        public ActionResult NuevoSistemaNavegacion()
        {
            var model = new SistemaNavegacionVM();
            model.Accion = AccionesController.Nuevo;
            return View("NuevoSistemaNavegacion", model);
        }

        public async Task<IActionResult> EditarSistemaNavegacion(int id)
        {
            var sistemaNavegacion = await _catalogoValorService.Obtener(id);
            var model = new SistemaNavegacionVM
            {
                IdCatalogo = sistemaNavegacion.IdCatalogo,
                IdSistemaNavegacion = sistemaNavegacion.IdCatalogoValor,
                SistemaNavegacion = sistemaNavegacion.Nombre,
                Descripcion = sistemaNavegacion.Descripcion,
                Accion = AccionesController.Editar
            };
            return View("NuevoSistemaNavegacion", model);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarSistemaNavegacion(SistemaNavegacionVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    AddAdvertencia(this.ErroresFromModel().Texto);
                    return View("NuevoSistemaNavegacion", model);
                }

                if (model.Accion == AccionesController.Nuevo)
                {
                    int idCatalogo = await ObtenerIdCatalogo();
                    var responseVerify = await _catalogoValorService.ValidarCatalogo(idCatalogo, model.SistemaNavegacion);
                    if (responseVerify)
                    {
                        AddAdvertencia("El Sistema de Navegacion que intentas registrar ya existe. Revisa la información e intenta nuevamente");
                        return View("NuevoSistemaNavegacion", model);
                    }
                    var tipoIdentificacion = new CatalogoValor
                    {
                        IdCatalogo = idCatalogo,
                        Nombre = model.SistemaNavegacion,
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
                        IdCatalogoValor = model.IdSistemaNavegacion,
                        Nombre = model.SistemaNavegacion,
                        Descripcion = model.Descripcion,
                        IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                        FechaModificacion = DateTime.Now,
                    };
                    var response = await _catalogoValorService.Actualizar(tipoIdentificacion);
                }

                AddExito(model.Accion == AccionesController.Nuevo
                                                                             ? "Sistema Navegacion registrada satisfactoriamente."
                                                                             : "Sistema Navegacion actualizada satisfactoriamente.");

                return RedirectToAction("Administrar");
            }
            catch (Exception)
            {
                AddError("Ha ocurrido un error. Contacte al Administrador.");
                return View("NuevoSistemaNavegacion", model);
                throw;
            }

        }
    }
}
