using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
    public interface IEmbarcacionService
    {
        Task<bool> Agregar(Embarcacion departamento);
        Task<bool> Actualizar(Embarcacion departamento);
        Task<bool> ValidarDuplicados(string valor, int? id = null);
        Task<bool> Eliminar(Embarcacion departamento);
        Task<Embarcacion> Obtener(int id);
        IQueryable<Embarcacion>? ObtenerTodos();
        Task<bool> ValidarEliminar(int id);
    }
}
