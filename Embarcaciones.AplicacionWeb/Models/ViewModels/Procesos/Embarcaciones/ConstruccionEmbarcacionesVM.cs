using Microsoft.AspNetCore.Mvc.Rendering;

namespace Embarcaciones.AplicacionWeb.Models.ViewModels.Procesos.Embarcaciones
{
    public class ConstruccionEmbarcacionesVM
    {
        public List<SelectListItem> ListaMaterial { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaColorM { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaColorV { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaColorSuperest { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaPropulsion { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaMarca { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaSistemaNavegacion { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaPuerto { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> ListaUndMedTRB { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaUndMedTRN { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaUndMedEslora { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaUndMedManga { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> ListaUndMedPuntal { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaUndCalado { get; set; } = new List<SelectListItem>();

        public int UndMedTRB { get; set; }
        public int UndMedTRN { get; set; }
        public int UndMedEslora { get; set; }
        public int UndMedManga { get; set; }
        public int UndMedPuntal { get; set; }
        public int UndMedCalado { get; set; }
        public int Material { get; set; }
        public int ColorM { get; set; }
        public int ColorV { get; set; }
        public int ColorSuperest { get; set; }
        public int Propulsion { get; set; }
        public int Marca { get; set; }
        public int SistemaNavegacion { get; set; }
        public int PuertoVisitado { get; set; }

        public string TRB { get; set; }
        public string TRN { get; set; }
        public string Eslora { get; set; }
        public string Manga { get; set; }
        public string Puntal { get; set; }
        public string Calado { get; set; }

        public DateOnly AnioConstruccion { get; set; }
        public int NumeroConstruccion { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public string Potencia { get; set; }
        public string TipoComunicacion { get; set; }
        public string MedioCx { get; set; }
        public string Frecuencia { get; set; }
        public string Indicativo { get; set; }
        public string NumeroTripulantes { get; set; }
        public string NumeroPasajeros { get; set; }
        public string CapacidadCarga { get; set; }
        public string TipoFechasInfracciones { get; set; }

    }
}

