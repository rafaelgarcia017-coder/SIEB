using Embarcaciones.DAL.DataContext;
using Embarcaciones.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.DAL.Repositorio
{
    public class CatalogoFormularioRepositorio:ICatalogoFormularioRepositorio
    {
        private readonly EmbarcacionesBDContext _dbcontext;

        public CatalogoFormularioRepositorio(EmbarcacionesBDContext context)
        {
            _dbcontext = context;
        }

        public IQueryable<Municipio> ObtenerMunicipioPorDepartamento(int id)
        {
            return _dbcontext.Municipio.Where(w => w.IdDepartamento == id && w.EstaActivo == true && w.EsHistorico == false);
        }
    }
}
