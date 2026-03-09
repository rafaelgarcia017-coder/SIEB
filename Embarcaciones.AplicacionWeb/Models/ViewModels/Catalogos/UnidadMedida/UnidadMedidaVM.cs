using Embarcaciones.AplicacionWeb.Models.Utils;
using System.ComponentModel.DataAnnotations;

namespace Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.UnidadMedida
{
    public class UnidadMedidaVM:FormViewModelBase
    {
        public int IdUnidadMedida { get; set; }

        [Required(ErrorMessage = "Por Favor Ingrese la Unidad Medida")]
        public string UnidadMedida { get; set; }

        [Required(ErrorMessage = "Por Favor Ingrese la Abreviatura")]
        public string Abreviatura { get; set; }

        public string? Descripcion { get; set; }

        public int? IdUsuarioCreacion { get; set; }

        public string? FechaCreacion { get; set; }

        public int? IdUsuarioModificacion { get; set; }

        public string? UsuarioCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

    }
}
