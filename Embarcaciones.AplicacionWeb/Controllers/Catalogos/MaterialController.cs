using Embarcaciones.AplicacionWeb.Models.Utils;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Material;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Embarcaciones.AplicacionWeb.Controllers.Catalogos
{
    public class MaterialController : CustomController
    {
        private readonly ICatalogoValorService _catalogoValorService;
        private readonly ICatalogoService _catalogoService;

        public MaterialController(ICatalogoValorService catalogoValorService, ICatalogoService catalogoService)
        {
            _catalogoValorService = catalogoValorService;
            _catalogoService = catalogoService;
        }

        private async Task<int> ObtenerIdCatalogo()
        {
            var id = await _catalogoService.ObtenerIdCatalogo("MTRL");
            return id;
        }
        public async Task< IActionResult> Administrar()
        {
            int idCatalogo = await ObtenerIdCatalogo();
            var obtenerRegistro = await _catalogoValorService.ObtenerTodos(idCatalogo).ToListAsync();

            var model = new AdministrarMaterialVM
            {
                ListaMaterial = obtenerRegistro.Select(s => new MaterialVM
                {
                    IdCatalogo = s.IdCatalogo,
                    IdMaterial = s.IdCatalogoValor,
                    Material = s.Nombre,
                    Descripcion = s.Descripcion,
                    FechaCreacion = s.FechaCreacion?.ToString("dd/MM/yyyy"),
                    UsuarioCreacion = s.UsuarioCreacionNavigation.Usuario
                }).ToList()

            };
            return View("Administrar", model);
        }
        public ActionResult NuevoMaterial()
        {
            var model = new MaterialVM();
            model.Accion = AccionesController.Nuevo;
            return View("NuevoMaterial", model);
        }
        public async Task<IActionResult> EditarMaterial(int id)
        {
            var tipoIdentificacion = await _catalogoValorService.Obtener(id);
            var model = new MaterialVM
            {
                IdCatalogo = tipoIdentificacion.IdCatalogo,
                IdMaterial = tipoIdentificacion.IdCatalogoValor,
                Material = tipoIdentificacion.Nombre,
                Descripcion = tipoIdentificacion.Descripcion,
                Accion = AccionesController.Editar
            };
            return View("NuevoMaterial", model);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarMaterial(MaterialVM model)
        {
            if (!ModelState.IsValid)
            {
                AddAdvertencia(this.ErroresFromModel().Texto);
                return View("NuevoMaterial", model);
            }

            if (model.Accion == AccionesController.Nuevo)
            {
                int idCatalogo = await ObtenerIdCatalogo();
                var responseVerify = await _catalogoValorService.ValidarCatalogo(idCatalogo, model.Material);
                if (responseVerify)
                {
                    AddAdvertencia("El Material que intentas registrar ya existe. Revisa la información e intenta nuevamente");
                    return View("NuevoMaterial", model);
                }
                var material = new CatalogoValor
                {
                    IdCatalogo = idCatalogo,
                    Nombre = model.Material,
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
                    IdCatalogoValor = model.IdMaterial,
                    Nombre = model.Material,
                    Descripcion = model.Descripcion,
                    IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    FechaModificacion = DateTime.Now,
                };
                var response = await _catalogoValorService.Actualizar(material);
            }


            return RedirectToAction("Administrar");
        }
    }
}
