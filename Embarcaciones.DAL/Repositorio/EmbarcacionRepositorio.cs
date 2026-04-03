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
using static Embarcaciones.Models.DashBoard;

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
            _dbcontext.Embarcacion.Attach(modelo);
            var entry = _dbcontext.Entry(modelo);
            // Marcar todo como modificado
            entry.State = EntityState.Modified;
            //  Excluir campos que NO se deben actualizar
            entry.Property(x => x.EstaActivo).IsModified = false;
            entry.Property(x => x.EsHistorico).IsModified = false;
            entry.Property(x => x.IdUsuarioCreacion).IsModified = false;
            entry.Property(x => x.FechaCreacion).IsModified = false;
            try
            {
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
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

        public async Task<DashBoard> ObtenerDashboard()
        {
            var dashboard = new DashBoard();
            var connectionString = _dbcontext.Database.GetDbConnection().ConnectionString;

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("Embarcaciones.prObtenerDashboard", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    // KPIs
                    if (await reader.ReadAsync())
                        dashboard.TotalEmbarcaciones = reader.GetInt32(0);

                    await reader.NextResultAsync();
                    if (await reader.ReadAsync())
                        dashboard.TotalPersonas = reader.GetInt32(0);

                    await reader.NextResultAsync();
                    if (await reader.ReadAsync())
                        dashboard.TotalNacionalidades = reader.GetInt32(0);

                    await reader.NextResultAsync();
                    if (await reader.ReadAsync())
                        dashboard.TotalPuertos = reader.GetInt32(0);

                    // Registros por mes
                    await reader.NextResultAsync();
                    while (await reader.ReadAsync())
                    {
                        dashboard.RegistrosMes.Add(new RegistroMesDto
                        {
                            Mes = reader.GetInt32(0),
                            Cantidad = reader.GetInt32(1)
                        });
                    }

                    // Tipos de embarcación
                    await reader.NextResultAsync();
                    while (await reader.ReadAsync())
                    {
                        dashboard.TiposEmbarcacion.Add(new TipoEmbarcacionDto
                        {
                            Tipo = reader.GetString(0),
                            Cantidad = reader.GetInt32(1)
                        });
                    }
                }
            }

            return dashboard;
        }
    }
}
