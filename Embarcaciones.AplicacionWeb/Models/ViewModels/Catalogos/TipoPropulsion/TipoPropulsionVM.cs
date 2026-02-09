using Embarcaciones.AplicacionWeb.Models.Utils;
using System.ComponentModel.DataAnnotations;

namespace Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.TipoPropulsion
{
    public class TipoPropulsionVM:FormViewModelBase
    {
        public int IdTipoPropulsion { get; set; }

        public int IdCatalogo { get; set; }

        [Required(ErrorMessage = "Por Favor Ingrese un Tipo de Propulsion")]
        public string ValorTipoPropulsion { get; set; }

        public string? Descripcion { get; set; }

        public int? IdUsuarioCreacion { get; set; }

        public string? FechaCreacion { get; set; }

        public int? IdUsuarioModificacion { get; set; }

        public string? UsuarioCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
