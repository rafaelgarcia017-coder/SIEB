using Embarcaciones.AplicacionWeb.Models.ViewModels
    .Catalogos.Departamento;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Persona;
using Embarcaciones.BLL.Service;
using Microsoft.AspNetCore.Mvc;

namespace Embarcaciones.AplicacionWeb.Controllers.Catalogos
{
    public class DepartamentoController : Controller
    {
        private readonly IDepartamentoService _departamentoService;

        public DepartamentoController(IDepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }
        public async Task <IActionResult> Administrar()
        {
            var obtenerRegistros = await  _departamentoService.ObtenerTodos().ToListAsync();
            var model = new AdministrarPersonasVM
            {
                ListaPersonas = obtenerRegistros
                   .Select(s => new PersonaVM
                   {
                       IdPersona = s.IdPersona,
                       NombreCompleto = s.NombreCompleto,
                       IdTipoIdentificacion = s.IdTipoIdentificacion,
                       Identificacion = s.Identificacion,
                       Direccion = s.Direccion,
                       Telefono = s.Telefono,
                       Correo = s.Correo,
                       FechaCreacion = s.FechaCreacion.ToString("dd/MM/yyyy"),
                       UsuarioCreacion = s.IdUsuarioCreacionNavigation?.Usuario ?? ""

                   }).ToList()
                // materializa la proyección directamente
            };

            //var depa = new List<DepartamentoVM> {
            //       new DepartamentoVM{ IdDepartamento = 1 , Departamento = "Managua", Descripcion="Departamento Managua", IdUsuarioCreacion = 1 , FechaCreacion = DateTime.Now},
            //       new DepartamentoVM{ IdDepartamento = 1 , Departamento = "Masaya", Descripcion="Departamento Masaya", IdUsuarioCreacion = 1 , FechaCreacion = DateTime.Now},
            //       new DepartamentoVM{ IdDepartamento = 1 , Departamento = "Granada", Descripcion="Departamento Granada", IdUsuarioCreacion = 1 , FechaCreacion = DateTime.Now},
            //       new DepartamentoVM{ IdDepartamento = 1 , Departamento = "Carazo", Descripcion="Departamento Carazo", IdUsuarioCreacion = 1 , FechaCreacion = DateTime.Now}
            //     };

            //var modelo = new AdministrarDepartamentoVM
            //{
            //    ListaDepartamentos = depa
            //};

            return View("Administrar", modelo);
        }

        public IActionResult NuevoDepartamento()
        {
            var model = new DepartamentoVM();
            return View("GestionDepartamento", model);
        }
    }
}
