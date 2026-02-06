using Embarcaciones.DAL.DataContext;
using Embarcaciones.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.DAL.Repositorio
{
    public class PersonaRepositorio : IPersonaRepositorio
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
                                                                                  .Include(i=> i.TipoIdentificacionNavigation)
                                                                                  .Where(w => (bool)w.EstaActivo);
            return queryPersona;
        }   

        public async Task<bool> ValidarPersonasDuplicadas(string nombreCompleto, string identificacion, int idTipoIdentificacion)
        {
            return await _dbcontext.Personas
                              .AnyAsync(a => (bool)a.EstaActivo == true &&
                                                      a.NombreCompleto == nombreCompleto &&
                                                      a.IdTipoIdentificacion == idTipoIdentificacion &&
                                                      a.Identificacion == identificacion
                                                     );
        }
    }
}
