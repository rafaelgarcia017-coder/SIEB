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
        Task<bool> Eliminar(Persona persona);
        Task<Persona> Obtener(int id);
        IQueryable<Persona> ObtenerTodos();
        Task<bool> ValidarDuplicados(string nombreCompleto, string? identificacion, int? idTipoIdentificacion, int? IdPersona);
        Task<Persona> ObtenerPorNombre(string nombre);
    }
}
