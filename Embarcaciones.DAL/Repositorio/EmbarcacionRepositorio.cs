using Embarcaciones.DAL.DataContext;
using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.DAL.Repositorio
{
    public class EmbarcacionRepositorio : IEmbarcacionRepositorio
    {
        private readonly EmbarcacionesBDContext _dbcontext;
        public EmbarcacionRepositorio(EmbarcacionesBDContext context) {
            _dbcontext = context;
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
                // ✅ Agregar solo la entidad raíz
                _dbcontext.Embarcacion.Add(modelo);

                // ✅ Un solo SaveChanges
                await _dbcontext.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
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

        Task<Embarcacion> IEmbarcacionRepositorio.Obtener(int id)
        {
            throw new NotImplementedException();
        }

        IQueryable<Embarcacion> IEmbarcacionRepositorio.ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
