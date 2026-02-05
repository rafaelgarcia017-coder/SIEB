using Embarcaciones.DAL.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
    public class CatalogoService : ICatalogoService
    {
        private readonly ICatalogoRepositorio _catalogoRepositorio;

        public CatalogoService(ICatalogoRepositorio catalogoRepositorio)
        {
            _catalogoRepositorio = catalogoRepositorio;
        }
        public async Task<int> ObtenerIdCatalogo(string codigoInterno)
        {
            return await _catalogoRepositorio.ObtenerIdCatalogo(codigoInterno);
        }
    }
}
