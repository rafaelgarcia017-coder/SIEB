using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.DAL.Repositorio
{
    public interface IPersonaRepositorio
    {
        Task<bool> Agregar(Persona modelo);
        Task<bool> Actualizar(Persona modelo);
        Task<bool> Eliminar(Persona modelo);
        Task<bool> ValidarPersonasDuplicadas(string nombreCompleto, string? identificacion, int? idTipoIdentificacion, int? idPersona );
        Task<Persona> Obtener(int id);
        IQueryable<Persona> ObtenerTodos();

        Task<bool> ValidarEliminar(int id);
    }
}
