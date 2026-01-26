using System;
using System.Collections.Generic;

namespace Embarcaciones.DAL.DataContext
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

        public virtual Cuentum IdUsuarioCreacionNavigation { get; set; } = null!;
        public virtual Cuentum? IdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<CatalogoValor> CatalogoValors { get; set; }
    }
}
