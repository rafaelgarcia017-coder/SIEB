using System;
using System.Collections.Generic;

namespace Embarcaciones.DAL.DataContext
{
    public partial class Cuentum
    {
        public Cuentum()
        {
            RolCuenta = new HashSet<RolCuentum>();
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

        public virtual ICollection<RolCuentum> RolCuenta { get; set; }
    }
}
