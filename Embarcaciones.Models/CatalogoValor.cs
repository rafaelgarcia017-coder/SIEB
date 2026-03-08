using Embarcaciones.Models;
using System;
using System.Collections.Generic;

namespace Embarcaciones.Models
{
    public partial class CatalogoValor
    {
        public CatalogoValor()
        {
            EmbarcacionConstruccionIdColorObraMuertaNavigation = new HashSet<EmbarcacionConstruccion>();
            EmbarcacionConstruccionIdColorObraVivaNavigation = new HashSet<EmbarcacionConstruccion>();
            EmbarcacionConstruccionIdColorSuperestructuraNavigation = new HashSet<EmbarcacionConstruccion>();
            EmbarcacionConstruccionIdMarcaMotorNavigation = new HashSet<EmbarcacionConstruccion>();
            EmbarcacionConstruccionIdPropulsionNavigation = new HashSet<EmbarcacionConstruccion>();
            EmbarcacionConstruccionIdSistemaNavegacionNavigation = new HashSet<EmbarcacionConstruccion>();
            EmbarcacionConstruccionIdTipoComunicacionNavigation = new HashSet<EmbarcacionConstruccion>();
            EmbarcacionIdActividadNavigation = new HashSet<Embarcacion>();
            EmbarcacionIdBanderaRegistroActualNavigation = new HashSet<Embarcacion>();
            EmbarcacionIdBanderaRegistroAnteriorNavigation = new HashSet<Embarcacion>();
            EmbarcacionIdPuertoRegistroActualNavigation = new HashSet<Embarcacion>();
            EmbarcacionIdPuertoRegistroAnteriorNavigation = new HashSet<Embarcacion>();
            EmbarcacionIdTipoEmbarcacionNavigation = new HashSet<Embarcacion>();
            EmbarcacionIdZonaNavegacionNavigation = new HashSet<Embarcacion>();
            EmbarcacionPropietario = new HashSet<EmbarcacionPropietario>();
            EmbarcacionPuertoSeleccion = new HashSet<EmbarcacionPuertoSeleccion>();
            Persona = new HashSet<Persona>();
        }

        public int IdCatalogoValor { get; set; }
        public int IdCatalogo { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool? EstaActivo { get; set; }
        public bool EsHistorico { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Catalogo IdCatalogoNavigation { get; set; } = null!;
        public virtual Cuenta UsuarioCreacionNavigation { get; set; } = null!;
        public virtual Cuenta? UsuarioModificacionNavigation { get; set; }
        public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdColorObraMuertaNavigation { get; set; }
        public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdColorObraVivaNavigation { get; set; }
        public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdColorSuperestructuraNavigation { get; set; }
        public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdMarcaMotorNavigation { get; set; }
        public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdPropulsionNavigation { get; set; }
        public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdSistemaNavegacionNavigation { get; set; }
        public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdTipoComunicacionNavigation { get; set; }
        public virtual ICollection<Embarcacion> EmbarcacionIdActividadNavigation { get; set; }
        public virtual ICollection<Embarcacion> EmbarcacionIdBanderaRegistroActualNavigation { get; set; }
        public virtual ICollection<Embarcacion> EmbarcacionIdBanderaRegistroAnteriorNavigation { get; set; }
        public virtual ICollection<Embarcacion> EmbarcacionIdPuertoRegistroActualNavigation { get; set; }
        public virtual ICollection<Embarcacion> EmbarcacionIdPuertoRegistroAnteriorNavigation { get; set; }
        public virtual ICollection<Embarcacion> EmbarcacionIdTipoEmbarcacionNavigation { get; set; }
        public virtual ICollection<Embarcacion> EmbarcacionIdZonaNavegacionNavigation { get; set; }
        public virtual ICollection<EmbarcacionPropietario> EmbarcacionPropietario { get; set; }
        public virtual ICollection<EmbarcacionPuertoSeleccion> EmbarcacionPuertoSeleccion { get; set; }
        public virtual ICollection<Persona> Persona { get; set; }
    }
}
