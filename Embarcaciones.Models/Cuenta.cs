using System;
using System.Collections.Generic;

namespace Embarcaciones.Models
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            RolCuenta = new HashSet<RolCuenta>();
        }

        public int IdCuenta { get; set; }
        public string Usuario { get; set; } = null!;
        public string ContrasenaHash { get; set; } = null!;
        public string? Estado { get; set; }
        public bool? EstaActivo { get; set; }
        public bool? EsHistorico { get; set; }
        public int? IdUsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual ICollection<RolCuenta> RolCuenta { get; set; }
    }
}
