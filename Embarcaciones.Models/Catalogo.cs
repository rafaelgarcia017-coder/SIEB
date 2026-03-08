using System;
using System.Collections.Generic;

namespace Embarcaciones.Models
{
    public partial class Catalogo
    {
        public Catalogo()
        {
            CatalogoValor = new HashSet<CatalogoValor>();
        }

        public int IdCatalogo { get; set; }
        public string CodigoInterno { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool? EstaActivo { get; set; }
        public bool EsHistorico { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Cuenta UsuarioCreacionNavigation { get; set; } = null!;
        public virtual Cuenta? UsuarioModificacionNavigation { get; set; }
        public virtual ICollection<CatalogoValor> CatalogoValor { get; set; }
    }
}
