using System.ComponentModel.DataAnnotations;

namespace Embarcaciones.AplicacionWeb.Models.ViewModels.Seguridad
{
    public class InicioSesionVM
    {
        [Required(ErrorMessage = "Por Favor Ingrese una Cuenta")]
        public string Cuenta { get; set; }

        [Required(ErrorMessage = "Por Favor Ingrese una Contraseña")]
        public string Clave { get; set; }
    }
}
