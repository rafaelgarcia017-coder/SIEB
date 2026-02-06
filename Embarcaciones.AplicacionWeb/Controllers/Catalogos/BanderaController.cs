using Embarcaciones.AplicacionWeb.Models.Utils;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Bandera;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Embarcaciones.AplicacionWeb.Controllers.Catalogos
{
    public class BanderaController : CustomController
    {
        private readonly ICatalogoValorService _catalogoValorService;
        private readonly ICatalogoService _catalogoService;

        public BanderaController(ICatalogoValorService catalogoValorService, ICatalogoService catalogoService)
        {
            _catalogoValorService = catalogoValorService;
            _catalogoService = catalogoService;
        }
        private async Task<int> ObtenerIdCatalogo()
        {
            var id = await _catalogoService.ObtenerIdCatalogo("BAND");
            return id;
        }
        public async Task< IActionResult >Administrar()
        {
            int idCatalogo = await ObtenerIdCatalogo();
            var obtenerRegistro = await _catalogoValorService.ObtenerTodos(idCatalogo).ToListAsync();

            var model = new AdministrarBanderaVM
            {
                ListaBandera = obtenerRegistro.Select(s => new BanderaVM
                {
                    IdCatalogo = s.IdCatalogo,
                    IdBandera = s.IdCatalogoValor,
                    Bandera = s.Nombre,
                    Descripcion = s.Descripcion,
                    FechaCreacion = s.FechaCreacion?.ToString("dd/MM/yyyy"),
                    UsuarioCreacion = s.UsuarioCreacionNavigation.Usuario
                }).ToList()

            };
            return View("Administrar", model);
        }

        public ActionResult NuevoBandera()
        {
            var model = new BanderaVM();
            model.Accion = AccionesController.Nuevo;
            return View("NuevoBandera", model);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarBandera(BanderaVM model)
        {
            if (!ModelState.IsValid)
            {
                AddAdvertencia(this.ErroresFromModel().Texto);
                return View("NuevoBandera", model);
            }

            if (model.Accion == AccionesController.Nuevo)
            {
                int idCatalogo = await ObtenerIdCatalogo();
                var responseVerify = await _catalogoValorService.ValidarCatalogo(idCatalogo, model.Bandera);
                if (responseVerify)
                {
                    AddAdvertencia("La Bandera que intentas registrar ya existe. Revisa la información e intenta nuevamente");
                    return View("NuevoBandera", model);
                }
                var bandera = new CatalogoValor
                {
                    IdCatalogo = idCatalogo,
                    Nombre = model.Bandera,
                    Descripcion = model.Descripcion,
                    IdUsuarioCreacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                };
                var response = await _catalogoValorService.Agregar(bandera);
            }
            else
            {
                var bandera = new CatalogoValor
                {
                    IdCatalogo = model.IdCatalogo,
                    IdCatalogoValor = model.IdBandera,
                    Nombre = model.Bandera,
                    Descripcion = model.Descripcion,
                    IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    FechaModificacion = DateTime.Now,
                };
                var response = await _catalogoValorService.Actualizar(bandera);
            }
            return RedirectToAction("Administrar");
        }
        public async Task<IActionResult> EditarBandera(int id)
        {
            var tipoIdentificacion = await _catalogoValorService.Obtener(id);
            var model = new BanderaVM
            {
                IdCatalogo = tipoIdentificacion.IdCatalogo,
                IdBandera = tipoIdentificacion.IdCatalogoValor,
                Bandera = tipoIdentificacion.Nombre,
                Descripcion = tipoIdentificacion.Descripcion,
                Accion = AccionesController.Editar
            };
            return View("NuevoBandera", model);
        }

    }
}
