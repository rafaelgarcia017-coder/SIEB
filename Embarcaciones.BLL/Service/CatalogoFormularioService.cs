using Embarcaciones.DAL.Repositorio;
using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
    public class CatalogoFormularioService:ICatalogoFormularioService
    {

        private readonly ICatalogoFormularioRepositorio _catalogoFormularioRepositorio;

        public CatalogoFormularioService(ICatalogoFormularioRepositorio catalogoformulariorepositorio)
        {
            _catalogoFormularioRepositorio = catalogoformulariorepositorio;
        }
        public IQueryable<Municipio> ObtenerMunicipioPorDepartamento(int id)
        {
            return _catalogoFormularioRepositorio.ObtenerMunicipioPorDepartamento(id);
        }

    }
}
