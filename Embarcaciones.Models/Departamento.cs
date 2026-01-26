using System;
using System.Collections.Generic;

namespace Embarcaciones.Models
{
    public partial class Departamento
    {
        public Departamento()
        {
            EmbarcacionPropietarios = new HashSet<EmbarcacionPropietario>();
            Municipios = new HashSet<Municipio>();
        }

        public int IdDepartamento { get; set; }
        public string Departamento1 { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool? EstaActivo { get; set; }
        public bool? EsHistorico { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Cuenta IdUsuarioCreacionNavigation { get; set; } = null!;
        public virtual Cuenta? IdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<EmbarcacionPropietario> EmbarcacionPropietarios { get; set; }
        public virtual ICollection<Municipio> Municipios { get; set; }
    }
}
