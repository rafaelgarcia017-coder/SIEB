using System;
using System.Collections.Generic;

namespace Embarcaciones.Models
{
    public partial class Persona
    {
        public int IdPersona { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public int? IdTipoIdentificacion { get; set; }
        public string? Identificacion { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }
        public bool? EstaActivo { get; set; }
        public bool? EsHistorico { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }         
        public virtual Cuenta IdUsuarioCreacionNavigation { get; set; } = null!;
        public virtual Cuenta? IdUsuarioModificacionNavigation { get; set; }
    }
}
