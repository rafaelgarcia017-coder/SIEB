using System;
using System.Collections.Generic;

namespace Embarcaciones.DAL.DataContext
{
    public partial class EmbarcacionPropietario
    {
        public EmbarcacionPropietario()
        {
            EmbarcacionIdEmbarcacionConstruccionNavigations = new HashSet<Embarcacion>();
            EmbarcacionIdEmbarcacionPropietarioNavigations = new HashSet<Embarcacion>();
        }

        public int IdEmbarcacionPropietario { get; set; }
        public string? NombrePropietario { get; set; }
        public int? IdNacionalidad { get; set; }
        public int? IdMunicipio { get; set; }
        public int? IdDepartamento { get; set; }
        public string? Domicilio { get; set; }
        public string? Telefono { get; set; }
        public string? EmpresaPropietaria { get; set; }
        public string? TelefonoEmpresaPropietaria { get; set; }
        public string? RutaImagenPropietario { get; set; }
        public string? LicenciaNavegacion { get; set; }
        public string? NumeroCarnetMarinero { get; set; }
        public string? NombreContacto { get; set; }
        public bool? EstaActivo { get; set; }
        public bool? EsHistorico { get; set; }
        public int? IdUsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Departamento? IdDepartamentoNavigation { get; set; }
        public virtual Municipio? IdMunicipioNavigation { get; set; }
        public virtual ICollection<Embarcacion> EmbarcacionIdEmbarcacionConstruccionNavigations { get; set; }
        public virtual ICollection<Embarcacion> EmbarcacionIdEmbarcacionPropietarioNavigations { get; set; }
    }
}
