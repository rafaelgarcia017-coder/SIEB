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
        Task<bool> Agregar(Departamento persona);
        Task<bool> Actualizar(Departamento persona);
        Task<bool> Eliminar(int id);
        Task<Departamento> Obtener(int id);
        IQueryable<Departamento>? ObtenerTodos();
    }
}
