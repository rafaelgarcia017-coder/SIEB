using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.DAL.Repositorio
{
    public interface ICatalogoRepositorio
    {
        Task<int> ObtenerIdCatalogo(string codigoInterno);
    }
}
