using System;
using System.Collections.Generic;

namespace Embarcaciones.DAL.DataContext
{
    public partial class Cuentum
    {
        public Cuentum()
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
            InverseIdUsuarioCreacionNavigation = new HashSet<Cuentum>();
            InverseIdUsuarioModificacionNavigation = new HashSet<Cuentum>();
            MunicipioIdUsuarioCreacionNavigations = new HashSet<Municipio>();
            MunicipioIdUsuarioModificacionNavigations = new HashSet<Municipio>();
            PaginaIdUsuarioCreacionNavigations = new HashSet<Pagina>();
            PaginaIdUsuarioModificacionNavigations = new HashSet<Pagina>();
            PersonaIdUsuarioCreacionNavigations = new HashSet<Persona>();
            PersonaIdUsuarioModificacionNavigations = new HashSet<Persona>();
            RolCuentumIdCuentaNavigations = new HashSet<RolCuentum>();
            RolCuentumIdUsuarioCreacionNavigations = new HashSet<RolCuentum>();
            RolCuentumIdUsuarioModificacionNavigations = new HashSet<RolCuentum>();
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

        public virtual Cuentum IdUsuarioCreacionNavigation { get; set; } = null!;
        public virtual Cuentum? IdUsuarioModificacionNavigation { get; set; }
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
        public virtual ICollection<Cuentum> InverseIdUsuarioCreacionNavigation { get; set; }
        public virtual ICollection<Cuentum> InverseIdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<Municipio> MunicipioIdUsuarioCreacionNavigations { get; set; }
        public virtual ICollection<Municipio> MunicipioIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<Pagina> PaginaIdUsuarioCreacionNavigations { get; set; }
        public virtual ICollection<Pagina> PaginaIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<Persona> PersonaIdUsuarioCreacionNavigations { get; set; }
        public virtual ICollection<Persona> PersonaIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<RolCuentum> RolCuentumIdCuentaNavigations { get; set; }
        public virtual ICollection<RolCuentum> RolCuentumIdUsuarioCreacionNavigations { get; set; }
        public virtual ICollection<RolCuentum> RolCuentumIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<Rol> RolIdUsuarioCreacionNavigations { get; set; }
        public virtual ICollection<Rol> RolIdUsuarioModificacionNavigations { get; set; }
    }
}
