using Embarcaciones.Models;
using System;
using System.Collections.Generic;

namespace Embarcaciones.Models
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            AccionIdUsuarioCreacionNavigation = new HashSet<Accion>();
            AccionIdUsuarioModificacionNavigation = new HashSet<Accion>();
            CatalogoIdUsuarioCreacionNavigation = new HashSet<Catalogo>();
            CatalogoIdUsuarioModificacionNavigation = new HashSet<Catalogo>();
            CatalogoValorIdUsuarioCreacionNavigation = new HashSet<CatalogoValor>();
            CatalogoValorIdUsuarioModificacionNavigation = new HashSet<CatalogoValor>();
            DepartamentoIdUsuarioCreacionNavigation = new HashSet<Departamento>();
            DepartamentoIdUsuarioModificacionNavigation = new HashSet<Departamento>();
            EmbarcacionConstruccionIdUsuarioCreacionNavigation = new HashSet<EmbarcacionConstruccion>();
            EmbarcacionConstruccionIdUsuarioModificacionNavigation = new HashSet<EmbarcacionConstruccion>();
            EmbarcacionIdUsuarioCreacionNavigation = new HashSet<Embarcacion>();
            EmbarcacionIdUsuarioModificacionNavigation = new HashSet<Embarcacion>();
            EmbarcacionPropietarioIdUsuarioCreacionNavigation = new HashSet<EmbarcacionPropietario>();
            EmbarcacionPropietarioIdUsuarioModificacionNavigation = new HashSet<EmbarcacionPropietario>();
            EmbarcacionPuertoSeleccionIdUsuarioCreacionNavigation = new HashSet<EmbarcacionPuertoSeleccion>();
            EmbarcacionPuertoSeleccionIdUsuarioModificacionNavigation = new HashSet<EmbarcacionPuertoSeleccion>();
            InverseIdUsuarioCreacionNavigation = new HashSet<Cuenta>();
            InverseIdUsuarioModificacionNavigation = new HashSet<Cuenta>();
            MunicipioIdUsuarioCreacionNavigation = new HashSet<Municipio>();
            MunicipioIdUsuarioModificacionNavigation = new HashSet<Municipio>();
            PaginaIdUsuarioCreacionNavigation = new HashSet<Pagina>();
            PaginaIdUsuarioModificacionNavigation = new HashSet<Pagina>();
            PersonaIdUsuarioCreacionNavigation = new HashSet<Persona>();
            PersonaIdUsuarioModificacionNavigation = new HashSet<Persona>();
            RolCuentaIdCuentaNavigation = new HashSet<RolCuenta>();
            RolCuentaIdUsuarioCreacionNavigation = new HashSet<RolCuenta>();
            RolCuentaIdUsuarioModificacionNavigation = new HashSet<RolCuenta>();
            RolIdUsuarioCreacionNavigation = new HashSet<Rol>();
            RolIdUsuarioModificacionNavigation = new HashSet<Rol>();
            RolPaginaAccionIdUsuarioCreacionNavigation = new HashSet<RolPaginaAccion>();
            RolPaginaAccionIdUsuarioModificacionNavigation = new HashSet<RolPaginaAccion>();
            UnidadMedidaIdUsuarioCreacionNavigation = new HashSet<UnidadMedida>();
            UnidadMedidaIdUsuarioModificacionNavigation = new HashSet<UnidadMedida>();
        }

        public int IdCuenta { get; set; }
        public int? IdPersona { get; set; }
        public string Usuario { get; set; } = null!;
        public byte[] ContrasenaHash { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public bool? EstaActivo { get; set; }
        public bool EsHistorico { get; set; }
        public int? IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Persona? IdPersonaNavigation { get; set; }
        public virtual Cuenta? UsuarioCreacionNavigation { get; set; }
        public virtual Cuenta? UsuarioModificacionNavigation { get; set; }
        public virtual ICollection<Accion> AccionIdUsuarioCreacionNavigation { get; set; }
        public virtual ICollection<Accion> AccionIdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<Catalogo> CatalogoIdUsuarioCreacionNavigation { get; set; }
        public virtual ICollection<Catalogo> CatalogoIdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<CatalogoValor> CatalogoValorIdUsuarioCreacionNavigation { get; set; }
        public virtual ICollection<CatalogoValor> CatalogoValorIdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<Departamento> DepartamentoIdUsuarioCreacionNavigation { get; set; }
        public virtual ICollection<Departamento> DepartamentoIdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdUsuarioCreacionNavigation { get; set; }
        public virtual ICollection<EmbarcacionConstruccion> EmbarcacionConstruccionIdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<Embarcacion> EmbarcacionIdUsuarioCreacionNavigation { get; set; }
        public virtual ICollection<Embarcacion> EmbarcacionIdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<EmbarcacionPropietario> EmbarcacionPropietarioIdUsuarioCreacionNavigation { get; set; }
        public virtual ICollection<EmbarcacionPropietario> EmbarcacionPropietarioIdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<EmbarcacionPuertoSeleccion> EmbarcacionPuertoSeleccionIdUsuarioCreacionNavigation { get; set; }
        public virtual ICollection<EmbarcacionPuertoSeleccion> EmbarcacionPuertoSeleccionIdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<Cuenta> InverseIdUsuarioCreacionNavigation { get; set; }
        public virtual ICollection<Cuenta> InverseIdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<Municipio> MunicipioIdUsuarioCreacionNavigation { get; set; }
        public virtual ICollection<Municipio> MunicipioIdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<Pagina> PaginaIdUsuarioCreacionNavigation { get; set; }
        public virtual ICollection<Pagina> PaginaIdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<Persona> PersonaIdUsuarioCreacionNavigation { get; set; }
        public virtual ICollection<Persona> PersonaIdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<RolCuenta> RolCuentaIdCuentaNavigation { get; set; }
        public virtual ICollection<RolCuenta> RolCuentaIdUsuarioCreacionNavigation { get; set; }
        public virtual ICollection<RolCuenta> RolCuentaIdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<Rol> RolIdUsuarioCreacionNavigation { get; set; }
        public virtual ICollection<Rol> RolIdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<RolPaginaAccion> RolPaginaAccionIdUsuarioCreacionNavigation { get; set; }
        public virtual ICollection<RolPaginaAccion> RolPaginaAccionIdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<UnidadMedida> UnidadMedidaIdUsuarioCreacionNavigation { get; set; }
        public virtual ICollection<UnidadMedida> UnidadMedidaIdUsuarioModificacionNavigation { get; set; }
    }
}
