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
    public class DepartamentoController : Controller
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
            return View("NuevoDepartamento", model);
        }
        [HttpPost]
        public async Task <IActionResult> NuevoDepartamento(DepartamentoVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("NuevoDepartamento", model);
            }

            var departamento = new Departamento
            {
                Departamento1 = model.Departamento,
                Descripcion = model.Descripcion,
                IdUsuarioCreacion = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            };
            var response = await _departamentoService.Agregar(departamento);


            return RedirectToAction("Administrar");
        }
    }
}
