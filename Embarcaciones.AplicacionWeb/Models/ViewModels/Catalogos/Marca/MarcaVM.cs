using Embarcaciones.AplicacionWeb.Models.Utils;
using System.ComponentModel.DataAnnotations;

namespace Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Marca
{
    public class MarcaVM:FormViewModelBase
    {
        public int IdMarca { get; set; }

        public int IdCatalogo { get; set; }

        [Required(ErrorMessage = "Por Favor Ingrese una Marca")]
        public string Marca { get; set; }

        public string? Descripcion { get; set; }

        public int? IdUsuarioCreacion { get; set; }

        public string? FechaCreacion { get; set; }

        public int? IdUsuarioModificacion { get; set; }

        public string? UsuarioCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }

}
