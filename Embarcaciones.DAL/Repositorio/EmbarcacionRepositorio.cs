using Embarcaciones.DAL.DataContext;
using Embarcaciones.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;

namespace Embarcaciones.DAL.Repositorio
{
    public class EmbarcacionRepositorio : IEmbarcacionRepositorio
    {
        private readonly EmbarcacionesBDContext _dbcontext;
        public EmbarcacionRepositorio(EmbarcacionesBDContext context)
        {
            _dbcontext = context;
        }
        public async Task<bool> Actualizar(Embarcacion modelo)
        {
             _dbcontext.Update(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;

        }

        public async Task<bool> Agregar(Embarcacion modelo)
        {
            _dbcontext.Embarcacion.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;

        }


        public Task<bool> Eliminar(Embarcacion modelo)
        {
            throw new NotImplementedException();
        }


        public async Task<Embarcacion> Obtener(int id)
        {
            return await _dbcontext.Embarcacion.FindAsync(id);
        }

        public async Task<IEnumerable<EmbarcacionDTO>> ObtenerTodos()
        {
            var connectionString = _dbcontext.Database.GetDbConnection().ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                return await connection.QueryAsync<EmbarcacionDTO>(
                    "Embarcaciones.prObtenerEmbarcaciones",
                    commandType: CommandType.StoredProcedure
                );
            }
        }

      
    }
}
