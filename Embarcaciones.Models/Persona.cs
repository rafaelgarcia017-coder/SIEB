using System;
using System.Collections.Generic;

namespace Embarcaciones.DAL.DataContext
{
    public partial class Persona
    {
        public Persona()
        {
            Cuenta = new HashSet<Cuenta>();
        }

        public int IdPersona { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public int IdTipoIdentificacion { get; set; }
        public string? Identificacion { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }
        public bool? EstaActivo { get; set; }
        public bool EsHistorico { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual CatalogoValor IdTipoIdentificacionNavigation { get; set; } = null!;
        public virtual Cuenta UsuarioCreacionNavigation { get; set; } = null!;
        public virtual Cuenta? UsuarioModificacionNavigation { get; set; }
        public virtual ICollection<Cuenta> Cuenta { get; set; }
    }
}
