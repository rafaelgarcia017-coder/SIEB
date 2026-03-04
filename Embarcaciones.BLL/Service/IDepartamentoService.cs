using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
  public  interface IDepartamentoService
    {
        Task<bool> Agregar(Departamento departamento);
        Task<bool> Actualizar(Departamento departamento);
        Task<bool> ValidarDuplicados(string valor, int? id = null);
        Task<bool> Eliminar(Departamento departamento);
        Task<Departamento> Obtener(int id);
        IQueryable<Departamento>? ObtenerTodos();
        Task<bool> ValidarEliminar(int id);
    }
}
