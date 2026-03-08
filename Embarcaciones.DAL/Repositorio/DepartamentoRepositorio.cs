using Embarcaciones.AplicacionWeb.Models.Utils;
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
    public class DepartamentoRepositorio : IGenericRepositorio<Departamento>
    {
        private readonly EmbarcacionesBDContext _dbcontext;
        public DepartamentoRepositorio(EmbarcacionesBDContext context)
        {
            _dbcontext = context;
        }

        public async Task<bool> Actualizar(Departamento modelo)
        {
            _dbcontext.Departamento.Attach(modelo);

            // Marcamos SOLO los campos que quieres modificar
            _dbcontext.Entry(modelo).Property(x => x.Departamento1).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.Descripcion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.FechaModificacion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.IdUsuarioModificacion).IsModified = true;

            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Agregar(Departamento modelo)
        {
            _dbcontext.Departamento.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(Departamento modelo)
        {
            _dbcontext.Departamento.Attach(modelo);

            // Marcamos SOLO los campos que quieres modificar
            _dbcontext.Entry(modelo).Property(x => x.EstaActivo).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.EsHistorico).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.FechaModificacion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.IdUsuarioModificacion).IsModified = true;

            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<Departamento> Obtener(int id)
        {
            return await _dbcontext.Departamento.FindAsync(id);
        }

        public IQueryable<Departamento> ObtenerTodos()
        {
            IQueryable<Departamento> queryDepartamento = _dbcontext.Departamento
                                                                                             .Include(d => d.UsuarioCreacionNavigation)
                                                                                             .Include(d => d.UsuarioModificacionNavigation)
                                                                                             .Where(w => (bool)w.EstaActivo);


            return queryDepartamento;
        }

        public async Task<bool> ValidarDuplicados(string valor, int? id)
        {
            //var valorLimpio = valor.CleanStringV2();

            // Compara con los registros activos normalizados
            return await _dbcontext.Departamento
           .AnyAsync(a => a.EstaActivo == true
                          && a.Departamento1 == valor
                          && (!id.HasValue || a.IdDepartamento != id.Value));
        }

        public async Task<bool> ValidarEliminar(int id)
        {      
                var connectionString = _dbcontext.Database.GetDbConnection().ConnectionString;

                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("Fundaciones.PermiteEliminarDepartamento", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@IdDepartamento", SqlDbType.Int).Value = id;

                    await connection.OpenAsync();

                    var result = await command.ExecuteScalarAsync();

                    return result != null && Convert.ToBoolean(result);
                }
            }
        
    }
}
