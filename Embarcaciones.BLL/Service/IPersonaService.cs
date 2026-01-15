using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
    public interface IPersonaService
    {
        Task<bool> Agregar(Persona persona);
        Task<bool> Actualizar(Persona persona);
        Task<bool> Eliminar(int id);
        Task<Persona> Obtener(int id);
        Task<IQueryable<Persona>> ObtenerTodos();
        Task<Persona> ObtenerPorNombre(string nombre);
    }
}
