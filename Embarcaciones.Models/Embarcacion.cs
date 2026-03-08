using Embarcaciones.Models;
using System;
using System.Collections.Generic;

namespace Embarcaciones.Models
{
    public partial class Embarcacion
    {
        public int IdEmbarcacion { get; set; }
        public int? IdEmbarcacionPropietario { get; set; }
        public int? IdEmbarcacionConstruccion { get; set; }
        public int? IdTipoEmbarcacion { get; set; }
        public int? IdPuertoRegistroAnterior { get; set; }
        public int IdPuertoRegistroActual { get; set; }
        public int IdBanderaRegistroActual { get; set; }
        public int? IdBanderaRegistroAnterior { get; set; }
        public int IdActividad { get; set; }
        public int? IdZonaNavegacion { get; set; }
        public string NombreActual { get; set; } = null!;
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
        public bool? EstaActivo { get; set; }
        public bool EsHistorico { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual CatalogoValor IdActividadNavigation { get; set; } = null!;
        public virtual CatalogoValor IdBanderaRegistroActualNavigation { get; set; } = null!;
        public virtual CatalogoValor? IdBanderaRegistroAnteriorNavigation { get; set; }
        public virtual EmbarcacionConstruccion? IdEmbarcacionConstruccionNavigation { get; set; }
        public virtual EmbarcacionPropietario? IdEmbarcacionPropietarioNavigation { get; set; }
        public virtual CatalogoValor IdPuertoRegistroActualNavigation { get; set; } = null!;
        public virtual CatalogoValor? IdPuertoRegistroAnteriorNavigation { get; set; }
        public virtual CatalogoValor? IdTipoEmbarcacionNavigation { get; set; }
        public virtual Cuenta UsuarioCreacionNavigation { get; set; } = null!;
        public virtual Cuenta? UsuarioModificacionNavigation { get; set; }
        public virtual CatalogoValor? IdZonaNavegacionNavigation { get; set; }
    }
}
