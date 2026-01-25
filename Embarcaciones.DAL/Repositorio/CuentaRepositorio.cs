using Embarcaciones.DAL.DataContext;
using Embarcaciones.
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.DAL.Repositorio
{
    public class CuentaRepositorio : ICuentaRepositorio
    {
        private readonly EmbarcacionesBDContext _dbcontext;

        public CuentaRepositorio(EmbarcacionesBDContext context)
        {
            _dbcontext = context;
        }

        // ---------- CRUD ----------
        public async Task<bool> Agregar(Cuenta modelo)
        {
            await _dbcontext.Cuenta.AddAsync(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Actualizar(Cuenta modelo)
        {
            _dbcontext.Cuenta.Update(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            var cuenta = await _dbcontext.Cuenta.FindAsync(id);
            if (cuenta == null) return false;

            _dbcontext.Cuenta.Remove(cuenta);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<Cuenta> Obtener(int id)
        {
            return await _dbcontext.Cuenta.FindAsync(id);
        }

        public IQueryable<Cuenta> ObtenerTodos()
        {
            return _dbcontext.Cuenta.AsQueryable();
        }

        // ---------- Método específico ----------
        public async Task<bool> ValidarCuenta(string cuenta, string clave)
        {
            return await _dbcontext.Cuenta
                .AnyAsync(c => c.Usuario == cuenta && c.ContrasenaHash == clave);
        }
    }
}
