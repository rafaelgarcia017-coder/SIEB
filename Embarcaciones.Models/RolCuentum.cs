using System;
using System.Collections.Generic;

namespace Embarcaciones.DAL.DataContext
{
    public partial class RolCuentum
    {
        public int IdRolCuenta { get; set; }
        public int IdCuenta { get; set; }
        public int IdRol { get; set; }
        public bool? EstaActivo { get; set; }
        public bool? EsHistorico { get; set; }
        public int? IdUsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Cuenta IdCuentaNavigation { get; set; } = null!;
        public virtual Rol IdRolNavigation { get; set; } = null!;
    }
}
