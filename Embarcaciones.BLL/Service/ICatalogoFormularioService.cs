using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
    public interface ICatalogoFormularioService
    {
        IQueryable<Municipio> ObtenerMunicipioPorDepartamento(int id);
    }
}
