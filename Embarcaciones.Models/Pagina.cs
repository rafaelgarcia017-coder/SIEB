using System;
using System.Collections.Generic;

namespace Embarcaciones.Models
{
    public partial class Pagina
    {
        public int IdPagina { get; set; }
        public string? Nombre { get; set; }
        public string? UrlPagina { get; set; }
        public string? Descripcion { get; set; }
        public bool? EstaActivo { get; set; }
        public bool? EsHistorico { get; set; }
        public int? IdUsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
