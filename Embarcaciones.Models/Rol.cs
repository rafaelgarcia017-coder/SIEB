using System;
using System.Collections.Generic;

namespace Embarcaciones.Models
{
    public partial class Rol
    {
        public Rol()
        {
            RolCuenta = new HashSet<RolCuenta>();
        }

        public int IdRol { get; set; }
        public string CodigoInterno { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual ICollection<RolCuenta> RolCuenta { get; set; }
    }
}
