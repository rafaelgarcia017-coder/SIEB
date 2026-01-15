using Embarcaciones.DAL.Repositorio;
using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
    public class PersonaService:IPersonaService
    {
        private readonly IGenericRepositorio<Persona> _personaRepo;
        public PersonaService(IGenericRepositorio<Persona> personaRepo)
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

        public async Task<bool> Eliminar(int id)
        {
            return await _personaRepo.Eliminar(id);
        }

        public async Task<Persona> Obtener(int id)
        {
            return await _personaRepo.Obtener(id);
        }

        public async Task<Persona> ObtenerPorNombre(string nombre)
        {
            IQueryable<Persona> queryPersonaSQL = await _personaRepo.ObtenerTodos();
            Persona persona = queryPersonaSQL.FirstOrDefault(f=> f.NombreCompleto == nombre);
            return persona;
        }

        public async Task<IQueryable<Persona>> ObtenerTodos()
        {
            return await _personaRepo.ObtenerTodos();
        }
    }
}
