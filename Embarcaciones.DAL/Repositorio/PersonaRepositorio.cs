using Embarcaciones.DAL.DataContext;
using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.DAL.Repositorio
{
    public class PersonaRepositorio : IGenericRepositorio<Persona>
    {
        private readonly EmbarcacionesBDContext _dbcontext;
        public PersonaRepositorio( EmbarcacionesBDContext context)
        {
            _dbcontext = context;
        }
        public async Task<bool> Actualizar(Persona modelo)
        {
            _dbcontext.Personas.Update(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public  async Task<bool> Agregar(Persona modelo)
        {
            _dbcontext.Personas.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }   

        public async Task<bool> Eliminar(int id)
        {
            Persona persona  = _dbcontext.Personas.FirstOrDefault(f => f.IdPersona == id);
            _dbcontext.Personas.Remove(persona);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<Persona> Obtener(int id)
        {
            return await _dbcontext.Personas.FindAsync(id);
        }

        public async Task<IQueryable<Persona>> ObtenerTodos()
        {
            IQueryable<Persona> queryPersona = _dbcontext.Personas;
            return queryPersona;
        }
    }
}
