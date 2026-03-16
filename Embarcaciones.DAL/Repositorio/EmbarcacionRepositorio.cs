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
            await _dbcontext.Embarcacion.AddAsync(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
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
