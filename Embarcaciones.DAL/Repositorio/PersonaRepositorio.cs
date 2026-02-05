using Embarcaciones.DAL.DataContext;
using Embarcaciones.Models;
using Microsoft.EntityFrameworkCore;
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
        public PersonaRepositorio(EmbarcacionesBDContext context)
        {
            _dbcontext = context;
        }
        public async Task<bool> Actualizar(Persona modelo)
        {
            _dbcontext.Personas.Attach(modelo);
           
            _dbcontext.Entry(modelo).Property(x=> x.NombreCompleto).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.Direccion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.Correo).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.Telefono).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.IdTipoIdentificacion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.Identificacion).IsModified = true;
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Agregar(Persona modelo)
        {
            _dbcontext.Personas.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            Persona persona = _dbcontext.Personas.FirstOrDefault(f => f.IdPersona == id);
            _dbcontext.Personas.Remove(persona);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<Persona> Obtener(int id)
        {
            return await _dbcontext.Personas.FindAsync(id);
        }

        public IQueryable<Persona> ObtenerTodos()
        {
            IQueryable<Persona> queryPersona = _dbcontext.Personas
                                                                                  .Include(d => d.UsuarioCreacionNavigation)
                                                                                  .Include(d => d.UsuarioModificacionNavigation)
                                                                                  .Where(w => (bool)w.EstaActivo);
            return queryPersona;
        }

        public Task<bool> ValidarDuplicados(string valor)
        {
            throw new NotImplementedException();
        }
    }
}
