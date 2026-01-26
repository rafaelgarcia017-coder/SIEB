using System;
using System.Collections.Generic;

namespace Embarcaciones.DAL.DataContext
{
    public partial class CatalogoValor
    {
        public int IdCatalogoValor { get; set; }
        public int IdCatalogo { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Catalogo IdCatalogoNavigation { get; set; } = null!;
        public virtual Cuentum IdUsuarioCreacionNavigation { get; set; } = null!;
        public virtual Cuentum? IdUsuarioModificacionNavigation { get; set; }
    }
}
