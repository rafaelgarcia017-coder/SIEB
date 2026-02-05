using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Departamento;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Persona;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace Embarcaciones.AplicacionWeb.Controllers.Catalogos
{
    public class PersonaController : Controller
    {

        private readonly IPersonaService _personaService;

        public PersonaController(IPersonaService personaService)
        {
            _personaService = personaService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Administrar()
        {
            var obtenerRegistros = await _personaService.ObtenerTodos().ToListAsync();
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
                       UsuarioCreacion = s.UsuarioCreacionNavigation?.Usuario ?? ""

                   }).ToList()
                // materializa la proyección directamente
            };

            return View("Administrar", model);
        }
        public IActionResult NuevaPersona()
        {
            var model = new PersonaVM();
            return View("NuevaPersona", model);
        }
        [HttpPost]
        public IActionResult NuevaPersona(PersonaVM model)
        {
            var user = User;
            if (ModelState.IsValid) {
                var persona = new Persona {
                  NombreCompleto = model.NombreCompleto,
                  Telefono = model.Telefono,
                  Direccion = model.Direccion,
                  Correo = model.Correo,
                  IdTipoIdentificacion = model.IdTipoIdentificacion,
                  Identificacion = model.Identificacion,
                  IdUsuarioCreacion  = Convert.ToInt32( User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                };
            }
            
            return View("NuevaPersona", model);
        }

        ////[HttpGet]
        ////public async Task<IActionResult> ObtenerPersonas()
        ////{
        ////    IQueryable<Persona> personas = await _personaService.ObtenerTodos();

        ////    var personasObtenidas = personas.Select(s => new PersonaVM()
        ////    {
        ////        IdPersona = s.IdPersona,
        ////        NombreCompleto = s.NombreCompleto,
        ////        Identificacion = s.Identificacion,
        ////        Direccion = s.Direccion,
        ////        Telefono = s.Telefono,
        ////        Correo = s.Correo,
        ////        FechaCreacion = s.FechaCreacion.ToString("dd/MM/yyyy"),
        ////        IdUsuarioCreacion = s.IdUsuarioCreacion
        ////    });

        ////    return StatusCode(StatusCodes.Status200OK, personasObtenidas);
        ////}
        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] PersonaVM model)
        {
            var persona = new Persona
            {
                NombreCompleto = model.NombreCompleto,
                Identificacion = model.Identificacion,
                Direccion = model.Direccion,
                Telefono = model.Telefono,
                Correo = model.Correo
            };

            bool response = await _personaService.Agregar(persona);

            return StatusCode(StatusCodes.Status200OK, new { valor = response });
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar([FromBody] PersonaVM model)
        {
            var persona = new Persona
            {
                NombreCompleto = model.NombreCompleto,
                Identificacion = model.Identificacion,
                Direccion = model.Direccion,
                Telefono = model.Telefono,
                Correo = model.Correo
            };

            bool response = await _personaService.Actualizar(persona);

            return StatusCode(StatusCodes.Status200OK, new { valor = response });
        }

    }
}
