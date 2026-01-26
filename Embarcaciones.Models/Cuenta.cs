using System;
using System.Collections.Generic;

namespace Embarcaciones.Models
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            CatalogoIdUsuarioCreacionNavigations = new HashSet<Catalogo>();
            CatalogoIdUsuarioModificacionNavigations = new HashSet<Catalogo>();
            CatalogoValorIdUsuarioCreacionNavigations = new HashSet<CatalogoValor>();
            CatalogoValorIdUsuarioModificacionNavigations = new HashSet<CatalogoValor>();
            DepartamentoIdUsuarioCreacionNavigations = new HashSet<Departamento>();
            DepartamentoIdUsuarioModificacionNavigations = new HashSet<Departamento>();
            EmbarcacionConstruccionIdUsuarioCreacionNavigations = new HashSet<EmbarcacionConstruccion>();
            EmbarcacionConstruccionIdUsuarioModificacionNavigations = new HashSet<EmbarcacionConstruccion>();
            EmbarcacionIdUsuarioCreacionNavigations = new HashSet<Embarcacion>();
            EmbarcacionIdUsuarioModificacionNavigations = new HashSet<Embarcacion>();
            EmbarcacionPropietarioIdUsuarioCreacionNavigations = new HashSet<EmbarcacionPropietario>();
            EmbarcacionPropietarioIdUsuarioModificacionNavigations = new HashSet<EmbarcacionPropietario>();
            InverseIdUsuarioCreacionNavigation = new HashSet<Cuenta>();
            InverseIdUsuarioModificacionNavigation = new HashSet<Cuenta>();
            MunicipioIdUsuarioCreacionNavigations = new HashSet<Municipio>();
            MunicipioIdUsuarioModificacionNavigations = new HashSet<Municipio>();
            PaginaIdUsuarioCreacionNavigations = new HashSet<Pagina>();
            PaginaIdUsuarioModificacionNavigations = new HashSet<Pagina>();
            PersonaIdUsuarioCreacionNavigations = new HashSet<Persona>();
            PersonaIdUsuarioModificacionNavigations = new HashSet<Persona>();
            RolCuentaIdCuentaNavigations = new HashSet<RolCuenta>();
            RolCuentaIdUsuarioCreacionNavigations = new HashSet<RolCuenta>();
            RolCuentaIdUsuarioModificacionNavigations = new HashSet<RolCuenta>();
            RolIdUsuarioCreacionNavigations = new HashSet<Rol>();
            RolIdUsuarioModificacionNavigations = new HashSet<Rol>();
        }

        public int IdCuenta { get; set; }
        public string Usuario { get; set; } = null!;
        public string ContrasenaHash { get; set; } = null!;
        public string? Estado { get; set; }
        public bool? EstaActivo { get; set; }
        public bool? EsHistorico { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Cuenta IdUsuarioCreacionNavigation { get; set; } = null!;
        public virtual Cuenta? IdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<Catalogo> CatalogoIdUsuarioCreacionNavigations { get; set; }
        public virtual ICollection<Catalogo> CatalogoIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<CatalogoValor> CatalogoValorIdUsuarioCreacionNavigations { get; set; }
        public virtual ICollection<CatalogoValor> CatalogoValorIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<Departamento> DepartamentoIdUsuarioCreacionNavigations { get; set; }
        public virtual ICollection<Departamento> DepartamentoIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdUsuarioCreacionNavigations { get; set; }
        public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<Embarcacion> EmbarcacionIdUsuarioCreacionNavigations { get; set; }
        public virtual ICollection<Embarcacion> EmbarcacionIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<EmbarcacionPropietario> EmbarcacionPropietarioIdUsuarioCreacionNavigations { get; set; }
        public virtual ICollection<EmbarcacionPropietario> EmbarcacionPropietarioIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<Cuenta> InverseIdUsuarioCreacionNavigation { get; set; }
        public virtual ICollection<Cuenta> InverseIdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<Municipio> MunicipioIdUsuarioCreacionNavigations { get; set; }
        public virtual ICollection<Municipio> MunicipioIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<Pagina> PaginaIdUsuarioCreacionNavigations { get; set; }
        public virtual ICollection<Pagina> PaginaIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<Persona> PersonaIdUsuarioCreacionNavigations { get; set; }
        public virtual ICollection<Persona> PersonaIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<RolCuenta> RolCuentaIdCuentaNavigations { get; set; }
        public virtual ICollection<RolCuenta> RolCuentaIdUsuarioCreacionNavigations { get; set; }
        public virtual ICollection<RolCuenta> RolCuentaIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<Rol> RolIdUsuarioCreacionNavigations { get; set; }
        public virtual ICollection<Rol> RolIdUsuarioModificacionNavigations { get; set; }
    }
}
