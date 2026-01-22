using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Departamento;
using Microsoft.AspNetCore.Mvc;

namespace Embarcaciones.AplicacionWeb.Controllers.Catalogos
{
    public class DepartamentoController : Controller
    {
        public IActionResult Administrar()
        {
            var depa = new List<DepartamentoVM> {
                   new DepartamentoVM{ IdDepartamento = 1 , Departamento = "Managua", Descripcion="Departamento Managua", IdUsuarioCreacion = 1 , FechaCreacion = DateTime.Now},
                   new DepartamentoVM{ IdDepartamento = 1 , Departamento = "Masaya", Descripcion="Departamento Masaya", IdUsuarioCreacion = 1 , FechaCreacion = DateTime.Now},
                   new DepartamentoVM{ IdDepartamento = 1 , Departamento = "Granada", Descripcion="Departamento Granada", IdUsuarioCreacion = 1 , FechaCreacion = DateTime.Now},
                   new DepartamentoVM{ IdDepartamento = 1 , Departamento = "Carazo", Descripcion="Departamento Carazo", IdUsuarioCreacion = 1 , FechaCreacion = DateTime.Now}
                 };

            return View("Administrar", depa);
        }
    }
}
