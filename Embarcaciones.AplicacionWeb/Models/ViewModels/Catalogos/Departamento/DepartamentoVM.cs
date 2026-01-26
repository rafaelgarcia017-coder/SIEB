using System.ComponentModel.DataAnnotations;

namespace Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Departamento
{
    public class DepartamentoVM
    {
        public int IdDepartamento { get; set; }

        [Required(ErrorMessage = "Por Favor Ingrese un Departamento")]
        public string Departamento { get; set; }

        public string? Descripcion { get; set; }

        public int? IdUsuarioCreacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? IdUsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
