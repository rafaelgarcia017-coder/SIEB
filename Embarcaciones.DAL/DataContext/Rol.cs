using System;
using System.Collections.Generic;

namespace Embarcaciones.DAL.DataContext
{
    public partial class Rol
    {
        public Rol()
        {
            RolCuenta = new HashSet<RolCuentum>();
        }

        public int IdRol { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool? EstaActivo { get; set; }
        public bool? EsHistorico { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Cuentum IdUsuarioCreacionNavigation { get; set; } = null!;
        public virtual Cuentum? IdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<RolCuentum> RolCuenta { get; set; }
    }
}
