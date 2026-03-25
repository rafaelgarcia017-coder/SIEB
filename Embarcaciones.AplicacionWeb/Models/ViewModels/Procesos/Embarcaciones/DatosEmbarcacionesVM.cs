using Microsoft.AspNetCore.Mvc.Rendering;

namespace Embarcaciones.AplicacionWeb.Models.ViewModels.Procesos.Embarcaciones
{
    public class DatosEmbarcacionesVM
    {
        public List<SelectListItem> ListaTipoEmbarcacion{ get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaPuertoRegistroAnterior { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaPuertoRegistroActual { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaBanderaActual { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaBanderaAnterior { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaActividad { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaZonaNavegacion { get; set; } = new List<SelectListItem>();
        public string MatriculaActual { get; set; }
        public string MatriculaAnterior { get; set; }
        public string NombreActual { get; set; }
        public string NombreAnterior { get; set; }
        public string NumeroOmi { get; set; }
        public string PropietarioAnterior { get; set; }
        public string IndicativoLLamada { get; set; }
        public DateTime? FechaAbanderamiento { get; set; }
        public string AnioInscripcion { get; set; }
        public string PermisoNavegacion { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public DateTime? FechaInscripcion { get; set; }
        public string LicenciaPesca { get; set; }
        public string LicenciaEspecialPesca { get; set; }
        public string Distrito { get; set; }
        public string Cap_Pce { get; set; }
        public string Observaciones { get; set; }
        public IFormFile ImagenFile { get; set; }
        public string UrlImagen { get; set; }

        public int? TipoEmbarcacion { get; set; }
        public int? PuertoRegistroAnterior { get; set; }
        public int PuertoRegistroActual { get; set; }
        public int BanderaActual { get; set; }

        public int? BanderaAnterior { get; set; }
        public int Actividad { get; set; }
        public int? ZonaNavegacion { get; set; }

    }
}
