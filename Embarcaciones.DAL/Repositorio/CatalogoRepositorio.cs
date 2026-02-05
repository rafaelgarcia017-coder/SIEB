using Embarcaciones.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.DAL.Repositorio
{
    public class CatalogoRepositorio : ICatalogoRepositorio
    {
        private readonly EmbarcacionesBDContext _dbcontext;

        public CatalogoRepositorio(EmbarcacionesBDContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        public async Task<int> ObtenerIdCatalogo(string codigoInterno)
        {
            return await _dbcontext.Catalogos
                                                    .Where(x => x.CodigoInterno == codigoInterno)
                                                    .Select(x => x.IdCatalogo)
                                                    .SingleAsync();
        }
    }
}
