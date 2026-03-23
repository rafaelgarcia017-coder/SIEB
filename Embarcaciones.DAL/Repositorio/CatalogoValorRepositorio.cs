using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Embarcaciones.Models;
using Embarcaciones.DAL.DataContext;

namespace Embarcaciones.DAL.Repositorio
{
    public class CatalogoValorRepositorio : ICatalogoValorRepositorio
    {
        private readonly EmbarcacionesBDContext _dbcontext;

        public CatalogoValorRepositorio(EmbarcacionesBDContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<bool> Actualizar(CatalogoValor modelo)
        {
            _dbcontext.CatalogoValor.Attach(modelo);

            _dbcontext.Entry(modelo).Property(x => x.Nombre).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.Descripcion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.FechaModificacion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.IdUsuarioModificacion).IsModified = true;

            await _dbcontext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Agregar(CatalogoValor modelo)
        {
            _dbcontext.CatalogoValor.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(CatalogoValor modelo)
        {
            _dbcontext.CatalogoValor.Attach(modelo);

            _dbcontext.Entry(modelo).Property(x => x.EstaActivo).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.EsHistorico).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.FechaModificacion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.IdUsuarioModificacion).IsModified = true;

            await _dbcontext.SaveChangesAsync();

            return true;
        }


        public async Task<CatalogoValor> Obtener(int id)
        {
            return await _dbcontext.CatalogoValor.FindAsync(id) ;
        }

        public IQueryable<CatalogoValor> ObtenerTodos(int idCatalogo)
        {
            IQueryable<CatalogoValor> queryCatalogo = _dbcontext.CatalogoValor
                                                                      .Where(w=> w.IdCatalogo == idCatalogo && w.EstaActivo == true && w.EsHistorico == false)
                                                                     .Include(d => d.UsuarioCreacionNavigation)
                                                                     .Include(d => d.UsuarioModificacionNavigation);

            return queryCatalogo;
        }

        public async Task<bool> ValidarCatalogo(int? idCatalogoValor, int idCatalogo, string valor)
        {   
            var query = _dbcontext.CatalogoValor .Where(a => a.Nombre == valor &&  a.IdCatalogo == idCatalogo && a.EstaActivo== true &&  !a.EsHistorico);

            if (idCatalogoValor.HasValue)
            {
                query = query.Where(a => a.IdCatalogoValor != idCatalogoValor.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<bool> ValidarEliminar(int idCatalogo, string codigoInternoCatalogo)
        {
            var connectionString = _dbcontext.Database.GetDbConnection().ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("Fundaciones.PermiteEliminarCatalogoValor", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@IdCatalogoValor", SqlDbType.Int).Value = idCatalogo;
                command.Parameters.Add("@CodigoInterno", SqlDbType.VarChar).Value = codigoInternoCatalogo;

                await connection.OpenAsync();

                var result = await command.ExecuteScalarAsync();

                return result != null && Convert.ToBoolean(result);
            }
        }

    }
}
