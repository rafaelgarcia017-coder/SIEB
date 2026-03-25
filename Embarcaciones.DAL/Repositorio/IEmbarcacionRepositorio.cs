using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.DAL.Repositorio
{
    public  interface IEmbarcacionRepositorio
    {
        Task<bool> Agregar(Embarcacion modelo);
        Task<bool> Actualizar(Embarcacion modelo);
        Task<bool> Eliminar(Embarcacion modelo);
        Task<Embarcacion> Obtener(int id);
        Task<IEnumerable<EmbarcacionDTO>> ObtenerTodos();
    }
}
