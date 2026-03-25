using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.Models
{
    public class EmbarcacionDTO
    {
        public int IdEmbarcacion { get; set; }
        public string Propietario { get; set; }
        public string Identificacion { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
        public string Licencia { get; set; }
        public string NombreActual { get; set; }
        public string Matricula { get; set; }
        public string OMI { get; set; }
        public string PermisoNavegacion { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }

}
