using Embarcaciones.Models;
using System;
using System.Collections.Generic;

namespace Embarcaciones.Models
{
    public partial class EmbarcacionConstruccion
    {
        public EmbarcacionConstruccion()
        {
            Embarcacion = new HashSet<Embarcacion>();
            EmbarcacionPuertoSeleccion = new HashSet<EmbarcacionPuertoSeleccion>();
        }

        public int IdEmbarcacionConstruccion { get; set; }
        public int? IdMaterial { get; set; }
        public int IdPropulsion { get; set; }
        public int? IdTipoComunicacion { get; set; }
        public int IdMarcaMotor { get; set; }
        public int? IdColorSuperestructura { get; set; }
        public int? IdColorObraMuerta { get; set; }
        public int? IdColorObraViva { get; set; }
        public int? IdSistemaNavegacion { get; set; }
        public decimal? Puntal { get; set; }
        public int IdUnidadMedidaPuntal { get; set; }
        public decimal? Trb { get; set; }
        public int IdUnidadMedidaTrb { get; set; }
        public decimal? Calado { get; set; }
        public int IdUnidadMedidaCalado { get; set; }
        public decimal? Trn { get; set; }
        public int IdUnidadMedidaTrn { get; set; }
        public decimal? Eslora { get; set; }
        public int IdUnidadMedidaEslora { get; set; }
        public decimal? Manga { get; set; }
        public int IdUnidadMedidaManga { get; set; }
        public string? SerieMotor { get; set; }
        public int? NumeroTripulantes { get; set; }
        public int? NumeroPasajeros { get; set; }
        public int? AnioConstruccion { get; set; }
        public string? Potencia { get; set; }
        public string? MediosCx { get; set; }
        public string? TipoFechaInfracciones { get; set; }
        public string? NumeroConstruccion { get; set; }
        public string? ModeloMotor { get; set; }
        public string? Frecuencia { get; set; }
        public string? CapacidadCarga { get; set; }
        public string? Indicativo { get; set; }
        public bool? EstaActivo { get; set; }
        public bool EsHistorico { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual CatalogoValor? IdColorObraMuertaNavigation { get; set; }
        public virtual CatalogoValor? IdColorObraVivaNavigation { get; set; }
        public virtual CatalogoValor? IdColorSuperestructuraNavigation { get; set; }
        public virtual CatalogoValor IdMarcaMotorNavigation { get; set; } = null!;
        public virtual CatalogoValor IdPropulsionNavigation { get; set; } = null!;
        public virtual CatalogoValor? IdSistemaNavegacionNavigation { get; set; }
        public virtual CatalogoValor? IdTipoComunicacionNavigation { get; set; }
        public virtual UnidadMedida IdUnidadMedidaCaladoNavigation { get; set; } = null!;
        public virtual UnidadMedida IdUnidadMedidaEsloraNavigation { get; set; } = null!;
        public virtual UnidadMedida IdUnidadMedidaMangaNavigation { get; set; } = null!;
        public virtual UnidadMedida IdUnidadMedidaPuntalNavigation { get; set; } = null!;
        public virtual UnidadMedida IdUnidadMedidaTrbNavigation { get; set; } = null!;
        public virtual UnidadMedida IdUnidadMedidaTrnNavigation { get; set; } = null!;
        public virtual Cuenta UsuarioCreacionNavigation { get; set; } = null!;
        public virtual Cuenta? UsuarioModificacionNavigation { get; set; }
        public virtual ICollection<Embarcacion> Embarcacion { get; set; }
        public virtual ICollection<EmbarcacionPuertoSeleccion> EmbarcacionPuertoSeleccion { get; set; }
    }
}
