using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.Models
{
    public class UnidadMedida
    {

        //public UnidadMedida()
        //{
        //    EmbarcacionConstruccionIdUnidadMedidaCaladoNavigations = new HashSet<EmbarcacionConstruccion>();
        //    EmbarcacionConstruccionIdUnidadMedidaEsloraNavigations = new HashSet<EmbarcacionConstruccion>();
        //    EmbarcacionConstruccionIdUnidadMedidaMangaNavigations = new HashSet<EmbarcacionConstruccion>();
        //    EmbarcacionConstruccionIdUnidadMedidaPuntalNavigations = new HashSet<EmbarcacionConstruccion>();
        //    EmbarcacionConstruccionIdUnidadMedidaTrbNavigations = new HashSet<EmbarcacionConstruccion>();
        //    EmbarcacionConstruccionIdUnidadMedidaTrnNavigations = new HashSet<EmbarcacionConstruccion>();
        //}

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

        //public virtual Cuenta IdUsuarioCreacionNavigation { get; set; } = null!;
        //public virtual Cuenta? IdUsuarioModificacionNavigation { get; set; }
        //public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdUnidadMedidaCaladoNavigations { get; set; }
        //public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdUnidadMedidaEsloraNavigations { get; set; }
        //public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdUnidadMedidaMangaNavigations { get; set; }
        //public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdUnidadMedidaPuntalNavigations { get; set; }
        //public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdUnidadMedidaTrbNavigations { get; set; }
        //public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdUnidadMedidaTrnNavigations { get; set; }
    }
}

