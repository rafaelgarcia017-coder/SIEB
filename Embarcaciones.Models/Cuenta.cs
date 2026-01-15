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
        public int IdPersona { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string PasswordHash { get; set; } = null!;
        public bool? Activo { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Persona IdPersonaNavigation { get; set; } = null!;
        public virtual ICollection<RolCuenta> RolCuenta { get; set; }
    }
}
