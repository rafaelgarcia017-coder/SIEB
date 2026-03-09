using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
    public interface IUnidadMedidaService
    {
        Task<bool> Agregar(UnidadMedida modelo);
        Task<bool> Actualizar(UnidadMedida modelo);
        Task<bool> Eliminar(UnidadMedida modelo);
        Task<bool> ValidarUnidadDuplicadas(string unidadmedidad, string? abreviatura, int? idUnidadMedida);
        Task<UnidadMedida> Obtener(int id);
        IQueryable<UnidadMedida> ObtenerTodos();
    }
}
