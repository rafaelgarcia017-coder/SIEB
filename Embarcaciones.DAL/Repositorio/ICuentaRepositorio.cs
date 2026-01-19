using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.DAL.Repositorio
{
  public  interface ICuentaRepositorio
    {
        // CRUD genérico
        Task<Cuenta> Obtener(int id);
        IQueryable<Cuenta> ObtenerTodos();
        Task<bool> Agregar(Cuenta entity);
        Task<bool> Actualizar(Cuenta entity);
        Task<bool> Eliminar(int id);

        // Método específico
        Task<bool> ValidarCuenta(string cuenta, string clave);
    }
}
