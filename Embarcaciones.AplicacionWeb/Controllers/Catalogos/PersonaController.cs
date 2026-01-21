using Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Persona;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var model = new AdministrarPersonasVM
            {
                ListaPersonas = await _personaService.ObtenerTodos()
                    .Select(s => new PersonaVM
                    {
                        IdPersona = s.IdPersona,
                        NombreCompleto = s.NombreCompleto,
                        Identificacion = s.Identificacion,
                        Direccion = s.Direccion,
                        Telefono = s.Telefono,
                        Correo = s.Correo,
                        FechaCreacion = s.FechaCreacion.ToString("dd/MM/yyyy"),
                        IdUsuarioCreacion = s.IdUsuarioCreacion
                    })
                    .ToListAsync() // materializa la proyección directamente
            };

            return View("Administrar", model);
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
