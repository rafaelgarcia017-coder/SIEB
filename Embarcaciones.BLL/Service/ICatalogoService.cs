using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
    public interface ICatalogoService
    {
        Task<int> ObtenerIdCatalogo(string codigoInterno);
    }
}
