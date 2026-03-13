using Embarcaciones.DAL.DataContext;
using Embarcaciones.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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
            _dbcontext.Municipio.Attach(modelo);

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
            _dbcontext.Municipio.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(Municipio modelo)
        {
            _dbcontext.Municipio.Attach(modelo);

            // Marcamos SOLO los campos que quieres modificar      
            _dbcontext.Entry(modelo).Property(x => x.EstaActivo).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.EsHistorico).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.FechaModificacion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.IdUsuarioModificacion).IsModified = true;

            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<Municipio> Obtener(int id)
        {
            return await _dbcontext.Municipio.FindAsync(id);
        }

        public IQueryable<Municipio> ObtenerTodos()
        {
            return _dbcontext.Municipio
                                                    .Include(d => d.UsuarioCreacionNavigation)
                                                    .Include(d => d.UsuarioModificacionNavigation)
                                                    .Include(d => d.DepartamentoNavigation)
                                                    .Where(w => w.EstaActivo == true)
                                                    .OrderBy(m => m.Municipio1);

        }

        public async Task<bool> ValidarDuplicados(string valor, int? id)
        {
            return await _dbcontext.Municipio
                .AnyAsync(a => a.EstaActivo == true
                               && a.Municipio1 == valor
                               && (!id.HasValue || a.IdMunicipio != id.Value));
        }

        public async Task<bool> ValidarEliminar(int id)
        {
            var connectionString = _dbcontext.Database.GetDbConnection().ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("Fundaciones.PermiteEliminarMunicipio", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@IdMunicipio", SqlDbType.Int).Value = id;

                await connection.OpenAsync();

                var result = await command.ExecuteScalarAsync();

                return result != null && Convert.ToBoolean(result);
            }
        }
    }
}
