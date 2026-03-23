using System;
using System.Collections.Generic;

namespace Embarcaciones.Models
{
    public partial class Rol
    {
        public Rol()
        {
            RolCuenta = new HashSet<RolCuenta>();
            RolPaginaAccion = new HashSet<RolPaginaAccion>();
        }

        public int IdRol { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool? EstaActivo { get; set; }
        public bool EsHistorico { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Cuenta IdUsuarioCreacionNavigation { get; set; } = null!;
        public virtual Cuenta? IdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<RolCuenta> RolCuenta { get; set; }
        public virtual ICollection<RolPaginaAccion> RolPaginaAccion { get; set; }
    }
}
