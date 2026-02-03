using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Municipio;
using Embarcaciones.BLL.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Embarcaciones.AplicacionWeb.Models.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;
using Embarcaciones.Models;
using System.Security.Claims;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Departamento;

namespace Embarcaciones.AplicacionWeb.Controllers.Catalogos
{
    public class MunicipioController : CustomController
    {
        private readonly IMunicipioService _municipioService;
        private readonly IDepartamentoService _departamentoService;

        public MunicipioController(IMunicipioService municipioService, IDepartamentoService departamentoService)
        {
            _municipioService = municipioService;
            _departamentoService = departamentoService;
        }
        public async Task<IActionResult> Administrar()
        {
            var obtenerRegistros = await _municipioService.ObtenerTodos().ToListAsync();

            var model = new AdministrarMunicipioVM
            {
                ListaMunicipios = obtenerRegistros.Select(s => new MunicipioVM
                {
                    IdMunicipio = s.IdMunicipio,
                    IdDepartamento = s.IdDepartamento,
                    Departamento = s.DepartamentoNavigation.Departamento1,
                    NombreMunicipio = s.Municipio1,
                    Descripcion = s.Descripcion,
                    FechaCreacion = s.FechaCreacion?.ToString("dd/MM/yyyy"),
                    UsuarioCreacion = s.UsuarioCreacionNavigation.Usuario
                }).ToList()
            };

            return View("Administrar", model);
        }
        public MunicipioVM LlenarModelo(MunicipioVM viewModel) {
            viewModel = viewModel ?? new MunicipioVM();
            var departamentos =  _departamentoService.ObtenerTodos().ToList();
            viewModel.ListaDepartamentos =  departamentos.Select(d => new SelectListItem
            {
                Value = d.IdDepartamento.ToString(),
                Text = d.Departamento1
            }).ToList();
            return viewModel;
        }
        public  IActionResult NuevoMunicipio()
        {
            var model =  LlenarModelo(new MunicipioVM());
            model.Accion = AccionesController.Nuevo;
            return View("NuevoMunicipio", model);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarMunicipio(MunicipioVM model) {
            if (!ModelState.IsValid)
            {
                AddAdvertencia(this.ErroresFromModel().Texto);
                return View("NuevoMunicipio", LlenarModelo(model));
            }
            if (model.Accion == AccionesController.Nuevo)
            {
                var responseVerify = await _municipioService.ValidarDuplicados(model.NombreMunicipio);
                if (responseVerify)
                {
                    AddAdvertencia("El Municipio que intentas registrar ya existe. Revisa la información e intenta nuevamente");
                    return View("NuevoMunicipio", LlenarModelo(model));
                }
                var municipio = new Municipio
                {
                    Municipio1 = model.NombreMunicipio,
                    IdDepartamento = model.IdDepartamento,
                    Descripcion = model.Descripcion,
                    IdUsuarioCreacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                };
                var response = await _municipioService.Agregar(municipio);
            }
            else
            {
                var municipio = new Municipio
                {
                    IdMunicipio = model.IdMunicipio,
                    IdDepartamento = model.IdDepartamento,
                    Municipio1 = model.NombreMunicipio,
                    Descripcion = model.Descripcion,
                    IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    FechaModificacion = DateTime.Now
                };
                var response = await _municipioService.Actualizar(municipio);
            }

            return RedirectToAction("Administrar");
        }
        public async Task<IActionResult> EditarMunicipio(int id)
        {
            var departamento = await _municipioService.Obtener(id);
            var model = LlenarModelo(new MunicipioVM());

            model.IdDepartamento = departamento.IdDepartamento;
            model.IdMunicipio = departamento.IdMunicipio;
            model.NombreMunicipio = departamento.Municipio1;
            model.Descripcion = departamento.Descripcion;
            model.Accion = AccionesController.Editar;
            
            return View("NuevoMunicipio", model);
        }
    }
}
