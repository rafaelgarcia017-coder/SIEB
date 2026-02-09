using Embarcaciones.AplicacionWeb.Models.Utils;
using System.ComponentModel.DataAnnotations;

namespace Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Material
{
    public class MaterialVM : FormViewModelBase
    {
        public int IdMaterial { get; set; }

        public int IdCatalogo { get; set; }

        [Required(ErrorMessage = "Por Favor Ingrese un Material")]
        public string Material { get; set; }

        public string? Descripcion { get; set; }

        public int? IdUsuarioCreacion { get; set; }

        public string? FechaCreacion { get; set; }

        public int? IdUsuarioModificacion { get; set; }

        public string? UsuarioCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
