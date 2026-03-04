using Embarcaciones.DAL.Repositorio;
using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
    public class PersonaService:IPersonaService
    {
        private readonly IPersonaRepositorio _personaRepo;
        public PersonaService(IPersonaRepositorio personaRepo)
        {
            _personaRepo = personaRepo;
        }

        public async Task<bool> Actualizar(Persona persona)
        {
            return await _personaRepo.Actualizar(persona);
        }

        public async Task<bool> Agregar(Persona persona)
        {
            return await _personaRepo.Agregar(persona);
        }

        public async Task<bool> Eliminar(Persona persona)
        {
            return await _personaRepo.Eliminar(persona);
        }

        public async Task<Persona> Obtener(int id)
        {
            return await _personaRepo.Obtener(id);
        }

        public async Task<Persona> ObtenerPorNombre(string nombre)
        {
            IQueryable<Persona> queryPersonaSQL =  _personaRepo.ObtenerTodos();
            Persona persona = queryPersonaSQL.FirstOrDefault(f=> f.NombreCompleto == nombre);
            return persona;
        }

        public IQueryable<Persona> ObtenerTodos()
        {
            return (IQueryable<Persona>)_personaRepo.ObtenerTodos();
        }  

        public async Task<bool> ValidarDuplicados(string nombreCompleto, string? identificacion, int? idTipoIdentificacion, int? idPersona)
        {
            return await _personaRepo.ValidarPersonasDuplicadas(nombreCompleto,identificacion, idTipoIdentificacion, idPersona);
        }
    }
}
