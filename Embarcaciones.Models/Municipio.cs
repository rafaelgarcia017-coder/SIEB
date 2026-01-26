using System;
using System.Collections.Generic;

namespace Embarcaciones.Models
{
    public partial class Municipio
    {
        public Municipio()
        {
            EmbarcacionPropietarios = new HashSet<EmbarcacionPropietario>();

        }

        public int IdMunicipio { get; set; }
        public int IdDepartamento { get; set; }
        public string Municipio1 { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool? EstaActivo { get; set; }
        public bool? EsHistorico { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
        public virtual Cuenta IdUsuarioCreacionNavigation { get; set; } = null!;
        public virtual Cuenta? IdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<EmbarcacionPropietario> EmbarcacionPropietarios { get; set; }
    }
}
