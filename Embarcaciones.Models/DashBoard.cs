using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.Models
{
   public class DashBoard
    {
        public int TotalEmbarcaciones { get; set; }
        public int TotalPersonas { get; set; }
        public int TotalPuertos { get; set; }
        public int TotalNacionalidades { get; set; }

        // Datos de gráficas
        public List<TipoEmbarcacionDto> TiposEmbarcacion { get; set; } = new List<TipoEmbarcacionDto>();
        public List<RegistroMesDto> RegistrosMes { get; set; } = new List<RegistroMesDto>();

        // Subclases internas para tipado fuerte
        public class TipoEmbarcacionDto
        {
            public string Tipo { get; set; }
            public int Cantidad { get; set; }
        }

        public class RegistroMesDto
        {
            public int Mes { get; set; }
            public int Cantidad { get; set; }
        }
    }
}
