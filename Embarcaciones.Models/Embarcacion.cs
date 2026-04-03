using System;
using System.Collections.Generic;

namespace Embarcaciones.Models
{
    public partial class Embarcacion
    {
        public int IdEmbarcacion { get; set; }
        public int IdTipoIdentificacion { get; set; }
        public string? Identificacion { get; set; }
        public int? IdNacionalidad { get; set; }
        public int IdMunicipio { get; set; }
        public int IdDepartamento { get; set; }
        public int? IdTipoEmbarcacion { get; set; }
        public int? IdPuertoRegistroAnterior { get; set; }
        public int IdPuertoRegistroActual { get; set; }
        public int IdBanderaRegistroActual { get; set; }
        public int? IdBanderaRegistroAnterior { get; set; }
        public int IdActividad { get; set; }
        public int? IdZonaNavegacion { get; set; }
        public int? IdMaterial { get; set; }
        public int? IdPropulsion { get; set; }
        public int? IdTipoComunicacion { get; set; }
        public int? IdMarcaMotor { get; set; }
        public int? IdColorSuperestructura { get; set; }
        public int? IdColorObraMuerta { get; set; }
        public int? IdColorObraViva { get; set; }
        public int? IdSistemaNavegacion { get; set; }
        public string NombrePropietario { get; set; } = null!;
        public string? NombreActual { get; set; }
        public string? PropietarioAnterior { get; set; }
        public DateTime? FechaAbanderada { get; set; }
        public DateTime? FechaInscripcion { get; set; }
        public string? PermisoNavegacion { get; set; }
        public string? LicenciaPesca { get; set; }
        public string? Distrito { get; set; }
        public string? MatriculaActual { get; set; }
        public string? NombreAnterior { get; set; }
        public string? IndicativoLlamada { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public string? LicenciaEspecialPesca { get; set; }
        public string? CapPce { get; set; }
        public string? MatriculaAnterior { get; set; }
        public string? NumeroOmi { get; set; }
        public string? Domicilio { get; set; }
        public string? Telefono { get; set; }
        public string? EmpresaPropietaria { get; set; }
        public string? TelefonoEmpresaPropietaria { get; set; }
        public string? RutaImagenPropietario { get; set; }
        public string? RutaImagenEmbarcacion { get; set; }
        public string? LicenciaNavegacion { get; set; }
        public string? NumeroCarnetMarinero { get; set; }
        public string? NombreContacto { get; set; }
        public decimal? Puntal { get; set; }
        public int? IdUnidadMedidaPuntal { get; set; }
        public decimal? Trb { get; set; }
        public int? IdUnidadMedidaTrb { get; set; }
        public decimal? Calado { get; set; }
        public int? IdUnidadMedidaCalado { get; set; }
        public decimal? Trn { get; set; }
        public int? IdUnidadMedidaTrn { get; set; }
        public decimal? Eslora { get; set; }
        public int? IdUnidadMedidaEslora { get; set; }
        public decimal? Manga { get; set; }
        public int? IdUnidadMedidaManga { get; set; }
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
    }
}
