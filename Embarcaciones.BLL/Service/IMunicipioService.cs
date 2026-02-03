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
        Task<bool> ValidarDuplicados(string valor);
        Task<bool> Eliminar(int id);
        Task<Municipio> Obtener(int id);
        IQueryable<Municipio>? ObtenerTodos();
    }
}
