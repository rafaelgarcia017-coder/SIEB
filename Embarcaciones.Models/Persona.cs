using System;
using System.Collections.Generic;

namespace Embarcaciones.Models
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
        public string Identificacion { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string Correo { get; set; } = null!;
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual ICollection<Cuenta> Cuenta { get; set; }
    }
}
