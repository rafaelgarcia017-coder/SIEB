using Embarcaciones.DAL.Repositorio;
using Embarcaciones.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
   public  class CuentaService : ICuentaService
    {
        private readonly ICuentaRepositorio _cuentaRepo;

        public CuentaService(ICuentaRepositorio cuentaRepo)
        {
            _cuentaRepo = cuentaRepo;
        }

        public async Task<Cuenta> Login(string cuenta, string clave)
        {
            return await _cuentaRepo.ValidarCuenta(cuenta, clave);
        }

        public async Task<Cuenta> Obtener(int id)
        {
            return await _cuentaRepo.Obtener(id);
        }

        public async Task<List<Cuenta>> ObtenerTodos()
        {
            return await _cuentaRepo.ObtenerTodos()
                                     .ToListAsync(); // ejecuta la query
        }

        public async Task<bool> Agregar(Cuenta cuenta)
        {
            return await _cuentaRepo.Agregar(cuenta);
        }

        public async Task<bool> Actualizar(Cuenta cuenta)
        {
            return await _cuentaRepo.Actualizar(cuenta);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _cuentaRepo.Eliminar(id);
        }


    }
}
