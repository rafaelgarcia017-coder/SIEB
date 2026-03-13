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
    public class UnidadMedidaRepositorio : IUnidadMedidaRepositorio
    {
        private readonly EmbarcacionesBDContext _dbcontext;
        public UnidadMedidaRepositorio(EmbarcacionesBDContext context)
        {
            _dbcontext = context;
        }

        public async Task<bool> Actualizar(UnidadMedida modelo)
        {
            _dbcontext.UnidadMedida.Attach(modelo);

            _dbcontext.Entry(modelo).Property(x => x.Nombre).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.Abreviatura).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.Descripcion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.IdUsuarioModificacion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.FechaModificacion).IsModified = true;
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public  async Task<bool> Agregar(UnidadMedida modelo)
        {
            _dbcontext.UnidadMedida.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(UnidadMedida modelo)
        {
            _dbcontext.UnidadMedida.Attach(modelo);
            _dbcontext.Entry(modelo).Property(x => x.EstaActivo).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.EsHistorico).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.FechaModificacion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.IdUsuarioModificacion).IsModified = true;
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<UnidadMedida> Obtener(int id)
        {
            return await _dbcontext.UnidadMedida.FindAsync(id);
        }

        public IQueryable<UnidadMedida> ObtenerTodos()
        {
            IQueryable<UnidadMedida> queryPersona = _dbcontext.UnidadMedida
                                                                       .Include(d => d.UsuarioCreacionNavigation)
                                                                       .Include(d => d.UsuarioModificacionNavigation)                                                    
                                                                       .Where(w => (bool)w.EstaActivo);
            return queryPersona;
        }

        public async Task<bool> ValidarUnidadDuplicadas(string unidadmedidad, string? abreviatura, int? idUnidadMedida)
        {
            var nombreNormalizado = unidadmedidad.Trim().ToUpper();
            var abreviaturaNormalizaado = abreviatura?.Trim().ToLower();

            // 1️⃣ Solo por nombre
            if (!string.IsNullOrWhiteSpace(nombreNormalizado) &&
                string.IsNullOrWhiteSpace(abreviaturaNormalizaado) &&
                !idUnidadMedida .HasValue)
            {
                return await _dbcontext.UnidadMedida.AnyAsync(a =>
                    a.EstaActivo == true &&
                    a.Nombre.Trim().ToUpper() == nombreNormalizado &&
                    (!idUnidadMedida.HasValue || a.IdUnidadMedida != idUnidadMedida.Value)
                );
            }
            // 2️⃣ Nombre + tipo de identificación (sin identificación)
            else if (!string.IsNullOrWhiteSpace(nombreNormalizado) &&
                     idUnidadMedida.HasValue &&
                     string.IsNullOrWhiteSpace(abreviaturaNormalizaado))
            {
                return await _dbcontext.UnidadMedida.AnyAsync(a =>
                    a.EstaActivo == true &&
                    a.Nombre.Trim().ToUpper() == nombreNormalizado &&
                    a.Abreviatura == abreviatura &&
                    (!idUnidadMedida.HasValue || a.IdUnidadMedida != idUnidadMedida.Value)
                );
            }
            // 3️⃣ Nombre + tipo + identificación
            else if (!string.IsNullOrWhiteSpace(nombreNormalizado) &&
                     idUnidadMedida.HasValue &&
                     !string.IsNullOrWhiteSpace(abreviaturaNormalizaado))
            {
                return await _dbcontext.UnidadMedida.AnyAsync(a =>
                    a.EstaActivo == true &&
                    a.Nombre.Trim().ToUpper() == nombreNormalizado &&
                    a.IdUnidadMedida == idUnidadMedida.Value &&
                    a.Abreviatura == abreviatura &&
                    (!idUnidadMedida.HasValue || a.IdUnidadMedida != idUnidadMedida.Value)
                );
            }

            // 4️⃣ Caso sin nada que validar
            return false;

        }
        public async Task<bool> ValidarEliminar(int id)
        {
            var connectionString = _dbcontext.Database.GetDbConnection().ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("Fundaciones.PermiteEliminarUnidadMedida", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@IdUnidadMedida", SqlDbType.Int).Value = id;

                await connection.OpenAsync();

                var result = await command.ExecuteScalarAsync();

                return result != null && Convert.ToBoolean(result);
            }
        }
    }
}
