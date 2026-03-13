using Embarcaciones.AplicacionWeb.Models.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Persona
{
    public class PersonaVM:FormViewModelBase
    {
        public int IdPersona { get; set; }
        [Required(ErrorMessage = "Por Favor Ingrese el Nombre Completo")]
        public string NombreCompleto { get; set; } = null!;
        [Required(ErrorMessage = "Por Favor Ingrese el Tipo De Identificacion")]
        public int TipoIdentificacion { get; set; }
        public string?  NombreTipoIdentificacion { get; set; }
        public string? Identificacion { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Correo { get; set; } = null!;
        public string? UsuarioCreacion { get; set; }
        public string? FechaCreacion { get; set; }
        public List<SelectListItem> ListaTipoIdentificacion { get; set; } = new List<SelectListItem>();
    }
}
