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
                // 1️⃣ Guardar Propietario
                _dbcontext.EmbarcacionPropietario.Add(modelo.EmbarcacionPropietarioNavigation);
                await _dbcontext.SaveChangesAsync();

                // 2️⃣ Guardar Construcción
                _dbcontext.EmbarcacionConstruccion.Add(modelo.EmbarcacionConstruccionNavigation);
                await _dbcontext.SaveChangesAsync();

                // 3️⃣ Asignar los IDs generados
                modelo.IdEmbarcacionPropietario = modelo.EmbarcacionPropietarioNavigation.IdEmbarcacionPropietario;
                modelo.IdEmbarcacionConstruccion = modelo.EmbarcacionConstruccionNavigation.IdEmbarcacionConstruccion;

                // 4️⃣ Guardar Embarcación
                _dbcontext.Embarcacion.Add(modelo);
                await _dbcontext.SaveChangesAsync();

                await transaction.CommitAsync();

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
