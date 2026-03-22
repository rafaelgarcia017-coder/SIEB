using Embarcaciones.DAL.DataContext;
using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.DAL.Repositorio
{
    public class EmbarcacionesRepositorio : IEmbarcacionesRepositorio
    {
        private readonly EmbarcacionesBDContext _dbcontext;

        public EmbarcacionesRepositorio (EmbarcacionesBDContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public Task<bool> Actualizar(Embarcacion modelo)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Agregar(Embarcacion modelo)
        {
            using var transaction = await _dbcontext.Database.BeginTransactionAsync();
            try
            {

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public Task<bool> Eliminar(Embarcacion modelo)
        {
            throw new NotImplementedException();
        }

        public Task<Embarcacion> Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Embarcacion> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
