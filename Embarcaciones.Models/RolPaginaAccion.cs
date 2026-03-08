using System;
using System.Collections.Generic;

namespace Embarcaciones.DAL.DataContext
{
    public partial class RolPaginaAccion
    {
        public int IdRolPaginaAccion { get; set; }
        public int IdRol { get; set; }
        public int IdPagina { get; set; }
        public int IdAccion { get; set; }
        public bool? EstaPermitido { get; set; }
        public bool? EstaActivo { get; set; }
        public bool EsHistorico { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Accion IdAccionNavigation { get; set; } = null!;
        public virtual Pagina IdPaginaNavigation { get; set; } = null!;
        public virtual Rol IdRolNavigation { get; set; } = null!;
        public virtual Cuenta UsuarioCreacionNavigation { get; set; } = null!;
        public virtual Cuenta? UsuarioModificacionNavigation { get; set; }
    }
}
