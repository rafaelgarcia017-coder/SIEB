namespace Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Persona
{
    public class PersonaVM
    {
        public int IdPersona { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public int? IdTipoIdentificacion { get; set; }
        public string? Identificacion { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Correo { get; set; } = null!;
        public string? UsuarioCreacion { get; set; }
        public string? FechaCreacion { get; set; }
    }
}
