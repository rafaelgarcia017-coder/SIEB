using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
   public interface ICatalogoValorService
    {
        Task<CatalogoValor> Obtener(int id);
        IQueryable<CatalogoValor> ObtenerTodos(int idCatalogo);
        Task<bool> Agregar(CatalogoValor entity);
        Task<bool> Actualizar(CatalogoValor entity);
        Task<bool> Eliminar(int id);
        Task<bool> ValidarCatalogo(int idCatalogo, string valor);
    }
}
