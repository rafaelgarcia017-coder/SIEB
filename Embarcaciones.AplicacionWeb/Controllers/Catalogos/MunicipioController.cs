using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Municipio;
using Embarcaciones.BLL.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Embarcaciones.AplicacionWeb.Models.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;
using Embarcaciones.Models;
using System.Security.Claims;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Departamento;
using Microsoft.Extensions.Logging;

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
        public async Task<IActionResult> GuardarMunicipio(MunicipioVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    AddAdvertencia(this.ErroresFromModel().Texto);
                    return View("NuevoMunicipio", LlenarModelo(model));
                }

                if (model.IdDepartamento == null)
                {
                    AddAdvertencia("Debe seleccionar un Departamento.");
                    return View("NuevoMunicipio", LlenarModelo(model));
                }

                if (model.Accion == AccionesController.Nuevo)
                {
                    if (await _municipioService.ValidarDuplicados(model.NombreMunicipio))
                    {
                        AddAdvertencia("El Municipio que intentas registrar ya existe. Revisa la información e intenta nuevamente");
                        return View("NuevoMunicipio", LlenarModelo(model));
                    }

                    var municipio = MapearMunicipio(model);
                    await _municipioService.Agregar(municipio);
                }
                else
                {
                    if (await _municipioService.ValidarDuplicados(model.NombreMunicipio,model.IdMunicipio))
                    {
                        AddAdvertencia("El Municipio que intentas actualizar ya existe. Revisa la información e intenta nuevamente");
                        return View("NuevoMunicipio", LlenarModelo(model));
                    }
                    // Para editar, opcional: validar duplicados también
                    var municipio = MapearMunicipio(model);
                    await _municipioService.Actualizar(municipio);
                }

                AddExito(model.Accion == AccionesController.Nuevo
                    ? "Municipio registrado satisfactoriamente."
                    : "Municipio actualizado satisfactoriamente.");

                return RedirectToAction("Administrar");
            }
            catch (Exception ex)
            {
                //ILogger.LogError(ex, "Error al guardar municipio");
                AddError("Ha ocurrido un error. Contacte al Administrador.");
                return View("NuevoMunicipio", LlenarModelo(model));
            }
        }

        // Método auxiliar
        private Municipio MapearMunicipio(MunicipioVM model)
        {
            var usuarioId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return new Municipio
            {
                IdMunicipio = model.IdMunicipio,
                IdDepartamento = model.IdDepartamento!.Value,
                Municipio1 = model.NombreMunicipio,
                Descripcion = model.Descripcion,
                IdUsuarioCreacion = model.Accion == AccionesController.Nuevo ? usuarioId : 0,
                IdUsuarioModificacion = model.Accion != AccionesController.Nuevo ? usuarioId : null,
                FechaModificacion = model.Accion != AccionesController.Nuevo ? DateTime.Now : null
            };
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
