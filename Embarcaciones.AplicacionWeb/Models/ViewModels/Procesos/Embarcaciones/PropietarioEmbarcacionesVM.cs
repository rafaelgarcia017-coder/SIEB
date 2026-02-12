using Microsoft.AspNetCore.Mvc.Rendering;

namespace Embarcaciones.AplicacionWeb.Models.ViewModels.Procesos.Embarcaciones
{
    public class PropietarioEmbarcacionesVM
    {
        public int IdEmbarcacionPropietario { get; set; }
        public string NombreCompleto { get; set; }
        public int TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public int Nacionalidad { get; set; }
        public int Departamento { get; set; }
        public int Municipio { get; set; }
        public string Domicilio { get; set; }
        public string LicenciaNavegacion { get; set; }
        public string Telefono { get; set; }
        public string NumeroCarnetMarinero { get; set; }
        public string EmpresaPropietario { get; set; }
        public string NombreContacto { get; set; }
        public string TelefonoContacto { get; set; }
        public string UrlImagen { get; set; }

        public IFormFile ImagenFile { get; set; }

        public List<SelectListItem> ListaDepartamentos { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaMunicipio { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaNacionalidad{ get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaTipoIdentificacion { get; set; } = new List<SelectListItem>();
    }
}
