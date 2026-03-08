using Embarcaciones.Models;
using System;
using System.Collections.Generic;

namespace Embarcaciones.Models
{
    public partial class UnidadMedida
    {
        public UnidadMedida()
        {
            EmbarcacionConstruccionIdUnidadMedidaCaladoNavigation = new HashSet<EmbarcacionConstruccion>();
            EmbarcacionConstruccionIdUnidadMedidaEsloraNavigation = new HashSet<EmbarcacionConstruccion>();
            EmbarcacionConstruccionIdUnidadMedidaMangaNavigation = new HashSet<EmbarcacionConstruccion>();
            EmbarcacionConstruccionIdUnidadMedidaPuntalNavigation = new HashSet<EmbarcacionConstruccion>();
            EmbarcacionConstruccionIdUnidadMedidaTrbNavigation = new HashSet<EmbarcacionConstruccion>();
            EmbarcacionConstruccionIdUnidadMedidaTrnNavigation = new HashSet<EmbarcacionConstruccion>();
        }

        public int IdUnidadMedida { get; set; }
        public string Nombre { get; set; } = null!;
        public string Abreviatura { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool? EstaActivo { get; set; }
        public bool EsHistorico { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Cuenta UsuarioCreacionNavigation { get; set; } = null!;
        public virtual Cuenta? UsuarioModificacionNavigation { get; set; }
        public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdUnidadMedidaCaladoNavigation { get; set; }
        public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdUnidadMedidaEsloraNavigation { get; set; }
        public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdUnidadMedidaMangaNavigation { get; set; }
        public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdUnidadMedidaPuntalNavigation { get; set; }
        public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdUnidadMedidaTrbNavigation { get; set; }
        public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdUnidadMedidaTrnNavigation { get; set; }
    }
}
