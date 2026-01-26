using System;
using System.Collections.Generic;

namespace Embarcaciones.Models
{
    public partial class Catalogo
    {
        public Catalogo()
        {
            CatalogoValors = new HashSet<CatalogoValor>();
        }

        public int IdCatalogo { get; set; }
        public string CodigoInterno { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Cuenta IdUsuarioCreacionNavigation { get; set; } = null!;
        public virtual Cuenta? IdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<CatalogoValor> CatalogoValors { get; set; }
    }
}
