using Embarcaciones.DAL.DataContext;
using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.DAL.Repositorio
{
    public class CuentaRepositorio : IGenericRepositorio<Cuenta>
    {
        private readonly EmbarcacionesBDContext _dbcontext;
        public CuentaRepositorio(EmbarcacionesBDContext context)
        { 
           _dbcontext = context;
        }
        public async Task<bool> Actualizar(Cuenta modelo)
        {
            _dbcontext.Cuenta.Update(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Agregar(Cuenta modelo)
        {
            _dbcontext.Cuenta.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public  async Task<bool> Eliminar(int id)
        {
            Cuenta cuenta = _dbcontext.Cuenta.FirstOrDefault(f => f.IdCuenta == id);
            _dbcontext.Cuenta.Remove(cuenta);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<Cuenta> Obtener(int id)
        {
            return await _dbcontext.Cuenta.FindAsync(id);
        }

        public async Task<IQueryable<Cuenta>> ObtenerTodos()
        {
            IQueryable<Cuenta> queryCuenta = _dbcontext.Cuenta;
            return queryCuenta;
        }
    }
}
