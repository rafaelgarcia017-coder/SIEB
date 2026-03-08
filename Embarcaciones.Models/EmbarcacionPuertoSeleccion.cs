using Embarcaciones.Models;
using System;
using System.Collections.Generic;

namespace Embarcaciones.Models
{
    public partial class EmbarcacionPuertoSeleccion
    {
        public int IdEmbarcacionPuertoSeleccion { get; set; }
        public int IdEmbarcacionConstruccion { get; set; }
        public int IdPuerto { get; set; }
        public bool? EstaActivo { get; set; }
        public bool EsHistorico { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual EmbarcacionConstruccion IdEmbarcacionConstruccionNavigation { get; set; } = null!;
        public virtual CatalogoValor IdPuertoNavigation { get; set; } = null!;
        public virtual Cuenta UsuarioCreacionNavigation { get; set; } = null!;
        public virtual Cuenta? UsuarioModificacionNavigation { get; set; }
    }
}
