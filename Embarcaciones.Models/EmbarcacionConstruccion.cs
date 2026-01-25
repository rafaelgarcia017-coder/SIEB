using System;
using System.Collections.Generic;

namespace Embarcaciones.DAL.DataContext
{
    public partial class EmbarcacionConstruccion
    {
        public int IdEmbarcacionConstruccion { get; set; }
        public decimal? Puntal { get; set; }
        public string? ColorSuperestructura { get; set; }
        public string? IdPropulsion { get; set; }
        public string? SerieMotor { get; set; }
        public int? IdTipoComunicacion { get; set; }
        public int? NumeroTripulantes { get; set; }
        public string? UltimoPuntoVisitado { get; set; }
        public decimal? Trb { get; set; }
        public decimal? Calado { get; set; }
        public int? AnioConstruccion { get; set; }
        public string? IdMarcaMotor { get; set; }
        public string? Potencia { get; set; }
        public string? MediosX { get; set; }
        public int? NumeroPasajeros { get; set; }
        public string? TipoFechaInfracciones { get; set; }
        public decimal? Trn { get; set; }
        public string? ColorObra { get; set; }
        public string? NumeroConstruccion { get; set; }
        public int? ModeloMotor { get; set; }
        public string? SistemaNavegacion { get; set; }
        public string? Frecuencia { get; set; }
        public decimal? CapacidadCarga { get; set; }
        public decimal? Eslora { get; set; }
        public decimal? Manga { get; set; }
        public string? ColorObraViva { get; set; }
        public string? Indicativo { get; set; }
        public bool? EstaActivo { get; set; }
        public bool? EsHistorico { get; set; }
        public int? IdUsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
