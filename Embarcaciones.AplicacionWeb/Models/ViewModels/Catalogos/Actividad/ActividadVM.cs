using Embarcaciones.AplicacionWeb.Models.Utils;
using System.ComponentModel.DataAnnotations;

namespace Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Actividad
{
    public class ActividadVM : FormViewModelBase
    {
        public int IdActividad { get; set; }

        public int IdCatalogo { get; set; }

        [Required(ErrorMessage = "Por Favor Ingrese una Actividad")]
        public string Actividad { get; set; }

        public string? Descripcion { get; set; }

        public int? IdUsuarioCreacion { get; set; }

        public string? FechaCreacion { get; set; }

        public int? IdUsuarioModificacion { get; set; }

        public string? UsuarioCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
