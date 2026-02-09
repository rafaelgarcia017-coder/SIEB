using Embarcaciones.AplicacionWeb.Models.Utils;
using System.ComponentModel.DataAnnotations;

namespace Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.SistemaNavegacion
{
    public class SistemaNavegacionVM:FormViewModelBase
    {
        public int IdSistemaNavegacion { get; set; }

        public int IdCatalogo { get; set; }

        [Required(ErrorMessage = "Por Favor Ingrese un Sistema de Navegacion")]
        public string SistemaNavegacion { get; set; }

        public string? Descripcion { get; set; }

        public int? IdUsuarioCreacion { get; set; }

        public string? FechaCreacion { get; set; }

        public int? IdUsuarioModificacion { get; set; }

        public string? UsuarioCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
