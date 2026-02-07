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
    public class MunicipioRepositorio : IGenericRepositorio<Municipio>
    {
        private readonly EmbarcacionesBDContext _dbcontext;

        public MunicipioRepositorio(EmbarcacionesBDContext context)
        {
            _dbcontext = context;
        }
        public async Task<bool> Actualizar(Municipio modelo)
        {
            _dbcontext.Municipios.Attach(modelo);

            // Marcamos SOLO los campos que quieres modificar
            _dbcontext.Entry(modelo).Property(x => x.IdDepartamento).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.Municipio1).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.Descripcion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.FechaModificacion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.IdUsuarioModificacion).IsModified = true;

            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Agregar(Municipio modelo)
        {
            _dbcontext.Municipios.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Municipio> Obtener(int id)
        {
            return await _dbcontext.Municipios.FindAsync(id);
        }

        public IQueryable<Municipio> ObtenerTodos()
        {
            return _dbcontext.Municipios
   .Include(d => d.UsuarioCreacionNavigation)
   .Include(d => d.UsuarioModificacionNavigation)
   .Include(d => d.DepartamentoNavigation)
   .Where(w => w.EstaActivo == true)
   .OrderBy(m => m.Municipio1);

        }

        public async Task<bool> ValidarDuplicados(string valor, int? id)
        {
            return await _dbcontext.Municipios
                .AnyAsync(a => a.EstaActivo == true
                               && a.Municipio1 == valor
                               && (!id.HasValue || a.IdMunicipio != id.Value));
        }
    }
}
