using Embarcaciones.AplicacionWeb.Models.Utils;
using Embarcaciones.AplicacionWeb.Models.ViewModels
    .Catalogos.Departamento;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Persona;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Embarcaciones.AplicacionWeb.Controllers.Catalogos
{
    public class DepartamentoController : CustomController
    {
        private readonly IDepartamentoService _departamentoService;

        public DepartamentoController(IDepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }
        public async Task<IActionResult> Administrar()
        {
            var obtenerRegistros = await _departamentoService.ObtenerTodos().ToListAsync();

            var model = new AdministrarDepartamentoVM
            {
                ListaDepartamentos = obtenerRegistros
                   .Select(s => new DepartamentoVM
                   {
                       IdDepartamento = s.IdDepartamento,
                       Departamento = s.Departamento1,
                       Descripcion = s.Descripcion,
                       FechaCreacion = s.FechaCreacion?.ToString("dd/MM/yyyy"),
                       UsuarioCreacion = s.UsuarioCreacionNavigation.Usuario
                   }).ToList()   
            };
            return View("Administrar", model);
        }

        public IActionResult NuevoDepartamento()
        {
            var model = new DepartamentoVM();
            model.Accion = AccionesController.Nuevo;
            return View("NuevoDepartamento", model);
        }
        [HttpPost]
        public async Task <IActionResult> GuardarDepartamento(DepartamentoVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    AddAdvertencia(this.ErroresFromModel().Texto);
                    return View("NuevoDepartamento", model);
                }
                if (model.Accion == AccionesController.Nuevo)
                {
                    var responseVerify = await _departamentoService.ValidarDuplicados(model.Departamento);
                    if (responseVerify)
                    {
                        AddAdvertencia("El departamento que intentas registrar ya existe. Revisa la información e intenta nuevamente");
                        return View("NuevoDepartamento", model);
                    }
                    var departamento = new Departamento
                    {
                        Departamento1 = model.Departamento,
                        Descripcion = model.Descripcion,
                        IdUsuarioCreacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                    };
                    var response = await _departamentoService.Agregar(departamento);
                }
                else
                {
                    var departamento = new Departamento
                    {
                        IdDepartamento = model.IdDepartamento,
                        Departamento1 = model.Departamento,
                        Descripcion = model.Descripcion,
                        IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                        FechaModificacion = DateTime.Now
                    };
                    var response = await _departamentoService.Actualizar(departamento);
                }
                AddExito(model.Accion == AccionesController.Nuevo
                                                                            ? "Departamento registrado satisfactoriamente."
                                                                            : "Departamento actualizado satisfactoriamente.");
                return RedirectToAction("Administrar");
            }
            catch (Exception)
            {
                AddError("Ha ocurrido un error. Contacte al Administrador.");
                return View("NuevoDepartamento", model);
                throw;
            }

        }

        public async Task< IActionResult> EditarDepartamento(int id)
        {           
            var departamento = await _departamentoService.Obtener(id);
            var model = new DepartamentoVM { 
             IdDepartamento = departamento.IdDepartamento,
             Departamento = departamento.Departamento1,
             Descripcion = departamento.Descripcion,
             Accion = AccionesController.Editar
            };
            return View("NuevoDepartamento", model);
        }

        [HttpPost]
        public async Task<ActionResult> EliminarDepartamento(int id)
        {
            try
            {
                var permiteEliminar = await _departamentoService.ValidarEliminar(id);

                if (!permiteEliminar)
                    return Json(new { success = false, mensaje = "No se puede eliminar este registro porque está asociado a otros datos. Para continuar, primero desvincule o elimine los registros relacionados." });

                var departamento = new Departamento
                {
                    IdDepartamento = id,
                    EstaActivo = false,
                    EsHistorico = true,
                    IdUsuarioModificacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    FechaModificacion = DateTime.Now
                };
                var responseVerify = await _departamentoService.Eliminar(departamento);
                if (responseVerify)
                    return Json(new { success = true, mensaje = "Se eliminó con éxito." });
                else
                    return Json(new { success = false, mensaje = "Ha ocurrido un error. Contacte al Administrador." });

            }
            catch (Exception)
            {
                return Json(new { success = false, mensaje = "Ha ocurrido un error. Contacte al Administrador." });
                throw;
            }

        }
    }
}
