using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
 public   interface IMunicipioService
    {
        Task<bool> Agregar(Municipio municipio);
        Task<bool> Actualizar(Municipio municipio);
        Task<bool> ValidarDuplicados(string valor, int? id = null);
        Task<bool> Eliminar(Municipio municipio);
        Task<Municipio> Obtener(int id);
        IQueryable<Municipio>? ObtenerTodos();
        Task<bool> ValidarEliminar(int id);
    }
}
