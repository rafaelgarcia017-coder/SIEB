using System;
using System.Collections.Generic;

namespace Embarcaciones.Models
{
    public partial class CatalogoValor
    {
        public int IdCatalogoValor { get; set; }
        public int IdCatalogo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? IdUsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Catalogo IdCatalogoNavigation { get; set; } = null!;
    }
}
