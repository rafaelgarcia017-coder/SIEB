using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Embarcaciones.Models;

namespace Embarcaciones.DAL.DataContext
{
    public partial class EmbarcacionesBDContext : DbContext
    {
        public EmbarcacionesBDContext()
        {
        }

        public EmbarcacionesBDContext(DbContextOptions<EmbarcacionesBDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Catalogo> Catalogos { get; set; } = null!;
        public virtual DbSet<CatalogoValor> CatalogoValors { get; set; } = null!;
        public virtual DbSet<Cuenta> Cuenta { get; set; } = null!;
        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Embarcacion> Embarcacions { get; set; } = null!;
        public virtual DbSet<EmbarcacionConstruccion> EmbarcacionConstruccions { get; set; } = null!;
        public virtual DbSet<EmbarcacionPropietario> EmbarcacionPropietarios { get; set; } = null!;
        public virtual DbSet<Municipio> Municipios { get; set; } = null!;
        public virtual DbSet<Pagina> Paginas { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<RolCuenta> RolCuenta { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-D1006TM\\MSSQLSERVER01;Database=EmbarcacionesBD;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catalogo>(entity =>
            {
                entity.HasKey(e => e.IdCatalogo)
                    .HasName("PK__Catalogo__FD0AC26C43E0F286");

                entity.ToTable("Catalogo", "Fundaciones");

                entity.Property(e => e.CodigoInterno).HasMaxLength(50);

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasOne(d => d.IdUsuarioCreacionNavigation)
                    .WithMany(p => p.CatalogoIdUsuarioCreacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Catalogo_Creacion");

                entity.HasOne(d => d.IdUsuarioModificacionNavigation)
                    .WithMany(p => p.CatalogoIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_Catalogo_Modificacion");
            });

            modelBuilder.Entity<CatalogoValor>(entity =>
            {
                entity.HasKey(e => e.IdCatalogoValor)
                    .HasName("PK__Catalogo__64C700622629D889");

                entity.ToTable("CatalogoValor", "Fundaciones");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasOne(d => d.IdCatalogoNavigation)
                    .WithMany(p => p.CatalogoValors)
                    .HasForeignKey(d => d.IdCatalogo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CatalogoValor_Catalogo");

                entity.HasOne(d => d.IdUsuarioCreacionNavigation)
                    .WithMany(p => p.CatalogoValorIdUsuarioCreacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CatalogoValor_Creacion");

                entity.HasOne(d => d.IdUsuarioModificacionNavigation)
                    .WithMany(p => p.CatalogoValorIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_CatalogoValor_Modificacion");
            });

            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(e => e.IdCuenta)
                    .HasName("PK__Cuenta__D41FD706D5D02AC1");

                entity.ToTable("Cuenta", "Seguridad");

                entity.HasIndex(e => e.Usuario, "UQ__Cuenta__E3237CF76FB9B3FB")
                    .IsUnique();

                entity.Property(e => e.ContrasenaHash).HasMaxLength(255);

                entity.Property(e => e.EsHistorico).HasDefaultValueSql("((0))");

                entity.Property(e => e.EstaActivo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Estado).HasMaxLength(20);

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Usuario).HasMaxLength(50);

                entity.HasOne(d => d.IdUsuarioCreacionNavigation)
                    .WithMany(p => p.InverseIdUsuarioCreacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cuenta_Creacion");

                entity.HasOne(d => d.IdUsuarioModificacionNavigation)
                    .WithMany(p => p.InverseIdUsuarioModificacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_Cuenta_Modificacion");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.IdDepartamento)
                    .HasName("PK__Departam__787A433DE50BF582");

                entity.ToTable("Departamento", "Fundaciones");

                entity.Property(e => e.Departamento1)
                    .HasMaxLength(100)
                    .HasColumnName("Departamento");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.EsHistorico).HasDefaultValueSql("((0))");

                entity.Property(e => e.EstaActivo).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.HasOne(d => d.IdUsuarioCreacionNavigation)
                    .WithMany(p => p.DepartamentoIdUsuarioCreacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Departamento_Creacion");

                entity.HasOne(d => d.IdUsuarioModificacionNavigation)
                    .WithMany(p => p.DepartamentoIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_Departamento_Modificacion");
            });

            modelBuilder.Entity<Embarcacion>(entity =>
            {
                entity.HasKey(e => e.IdEmbarcacion)
                    .HasName("PK__Embarcac__D9F3219E0DA49C5A");

                entity.ToTable("Embarcacion", "Embarcaciones");

                entity.Property(e => e.CapPce)
                    .HasMaxLength(50)
                    .HasColumnName("CAP_PCE");

                entity.Property(e => e.Distrito).HasMaxLength(50);

                entity.Property(e => e.EsHistorico).HasDefaultValueSql("((0))");

                entity.Property(e => e.EstaActivo).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaAbanderada).HasColumnType("date");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.FechaExpiracion).HasColumnType("date");

                entity.Property(e => e.FechaInscripcion).HasColumnType("date");

                entity.Property(e => e.IndicativoLlamada).HasMaxLength(50);

                entity.Property(e => e.LicenciaEspecialPesca).HasMaxLength(50);

                entity.Property(e => e.LicenciaPesca).HasMaxLength(50);

                entity.Property(e => e.MatriculaActual).HasMaxLength(50);

                entity.Property(e => e.MatriculaAnterior).HasMaxLength(50);

                entity.Property(e => e.NombreActual).HasMaxLength(100);

                entity.Property(e => e.NombreAnterior).HasMaxLength(100);

                entity.Property(e => e.NumeroOmi)
                    .HasMaxLength(50)
                    .HasColumnName("NumeroOMI");

                entity.Property(e => e.PermisoNavegacion).HasMaxLength(50);

                entity.Property(e => e.PropietarioAnterior).HasMaxLength(150);

                entity.Property(e => e.ZonaNavegacion).HasMaxLength(50);

                entity.HasOne(d => d.IdEmbarcacionConstruccionNavigation)
                    .WithMany(p => p.Embarcacions)
                    .HasForeignKey(d => d.IdEmbarcacionConstruccion)
                    .HasConstraintName("FK_Embarcacion_EmbConst");

                entity.HasOne(d => d.IdEmbarcacionPropietarioNavigation)
                    .WithMany(p => p.Embarcacions)
                    .HasForeignKey(d => d.IdEmbarcacionPropietario)
                    .HasConstraintName("FK_Embarcacion_EmbProp");

                entity.HasOne(d => d.IdUsuarioCreacionNavigation)
                    .WithMany(p => p.EmbarcacionIdUsuarioCreacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Embarcacion_Creacion");

                entity.HasOne(d => d.IdUsuarioModificacionNavigation)
                    .WithMany(p => p.EmbarcacionIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_Embarcacion_Modificacion");
            });

            modelBuilder.Entity<EmbarcacionConstruccion>(entity =>
            {
                entity.HasKey(e => e.IdEmbarcacionConstruccion)
                    .HasName("PK__Embarcac__189A72FD02E8FF43");

                entity.ToTable("EmbarcacionConstruccion", "Embarcaciones");

                entity.Property(e => e.Calado).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CapacidadCarga).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ColorObra).HasMaxLength(50);

                entity.Property(e => e.ColorObraViva).HasMaxLength(50);

                entity.Property(e => e.ColorSuperestructura).HasMaxLength(50);

                entity.Property(e => e.EsHistorico).HasDefaultValueSql("((0))");

                entity.Property(e => e.Eslora).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.EstaActivo).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Frecuencia).HasMaxLength(50);

                entity.Property(e => e.IdMarcaMotor).HasMaxLength(50);

                entity.Property(e => e.IdPropulsion).HasMaxLength(50);

                entity.Property(e => e.Indicativo).HasMaxLength(50);

                entity.Property(e => e.Manga).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.MediosX).HasMaxLength(50);

                entity.Property(e => e.NumeroConstruccion).HasMaxLength(50);

                entity.Property(e => e.Potencia).HasMaxLength(50);

                entity.Property(e => e.Puntal).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.SerieMotor).HasMaxLength(50);

                entity.Property(e => e.SistemaNavegacion).HasMaxLength(50);

                entity.Property(e => e.TipoFechaInfracciones).HasMaxLength(50);

                entity.Property(e => e.Trb)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("TRB");

                entity.Property(e => e.Trn)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("TRN");

                entity.Property(e => e.UltimoPuntoVisitado).HasMaxLength(100);

                entity.HasOne(d => d.IdUsuarioCreacionNavigation)
                    .WithMany(p => p.EmbarcacionConstruccionIdUsuarioCreacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmbConst_Creacion");

                entity.HasOne(d => d.IdUsuarioModificacionNavigation)
                    .WithMany(p => p.EmbarcacionConstruccionIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_EmbConst_Modificacion");
            });

            modelBuilder.Entity<EmbarcacionPropietario>(entity =>
            {
                entity.HasKey(e => e.IdEmbarcacionPropietario)
                    .HasName("PK__Embarcac__9A7F72A5C7C14CBB");

                entity.ToTable("EmbarcacionPropietario", "Embarcaciones");

                entity.Property(e => e.Domicilio).HasMaxLength(255);

                entity.Property(e => e.EmpresaPropietaria).HasMaxLength(150);

                entity.Property(e => e.EsHistorico).HasDefaultValueSql("((0))");

                entity.Property(e => e.EstaActivo).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LicenciaNavegacion).HasMaxLength(50);

                entity.Property(e => e.NombreContacto).HasMaxLength(150);

                entity.Property(e => e.NombrePropietario).HasMaxLength(150);

                entity.Property(e => e.NumeroCarnetMarinero).HasMaxLength(50);

                entity.Property(e => e.RutaImagenPropietario).HasMaxLength(255);

                entity.Property(e => e.Telefono).HasMaxLength(30);

                entity.Property(e => e.TelefonoEmpresaPropietaria).HasMaxLength(30);

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.EmbarcacionPropietarios)
                    .HasForeignKey(d => d.IdDepartamento)
                    .HasConstraintName("FK_EmbProp_Departamento");

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.EmbarcacionPropietarios)
                    .HasForeignKey(d => d.IdMunicipio)
                    .HasConstraintName("FK_EmbProp_Municipio");

                entity.HasOne(d => d.IdUsuarioCreacionNavigation)
                    .WithMany(p => p.EmbarcacionPropietarioIdUsuarioCreacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmbProp_Creacion");

                entity.HasOne(d => d.IdUsuarioModificacionNavigation)
                    .WithMany(p => p.EmbarcacionPropietarioIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_EmbProp_Modificacion");
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.HasKey(e => e.IdMunicipio)
                    .HasName("PK__Municipi__61005978D35AF7A6");

                entity.ToTable("Municipio", "Fundaciones");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.EsHistorico).HasDefaultValueSql("((0))");

                entity.Property(e => e.EstaActivo).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Municipio1)
                    .HasMaxLength(100)
                    .HasColumnName("Municipio");

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Municipios)
                    .HasForeignKey(d => d.IdDepartamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Municipio_Departamento");

                entity.HasOne(d => d.IdUsuarioCreacionNavigation)
                    .WithMany(p => p.MunicipioIdUsuarioCreacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Municipio_Creacion");

                entity.HasOne(d => d.IdUsuarioModificacionNavigation)
                    .WithMany(p => p.MunicipioIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_Municipio_Modificacion");
            });

            modelBuilder.Entity<Pagina>(entity =>
            {
                entity.HasKey(e => e.IdPagina)
                    .HasName("PK__Pagina__034F88B86B2BB937");

                entity.ToTable("Pagina", "Seguridad");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.EsHistorico).HasDefaultValueSql("((0))");

                entity.Property(e => e.EstaActivo).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.UrlPagina).HasMaxLength(255);

                entity.HasOne(d => d.IdUsuarioCreacionNavigation)
                    .WithMany(p => p.PaginaIdUsuarioCreacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pagina_Creacion");

                entity.HasOne(d => d.IdUsuarioModificacionNavigation)
                    .WithMany(p => p.PaginaIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_Pagina_Modificacion");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona)
                    .HasName("PK__Persona__2EC8D2AC3A4E9653");

                entity.ToTable("Persona", "Fundaciones");

                entity.Property(e => e.Correo).HasMaxLength(100);

                entity.Property(e => e.Direccion).HasMaxLength(255);

                entity.Property(e => e.EsHistorico).HasDefaultValueSql("((0))");

                entity.Property(e => e.EstaActivo).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Identificacion).HasMaxLength(50);

                entity.Property(e => e.NombreCompleto).HasMaxLength(150);

                entity.Property(e => e.Telefono).HasMaxLength(30);

                entity.HasOne(d => d.IdUsuarioCreacionNavigation)
                    .WithMany(p => p.PersonaIdUsuarioCreacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Persona_Creacion");

                entity.HasOne(d => d.IdUsuarioModificacionNavigation)
                    .WithMany(p => p.PersonaIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_Persona_Modificacion");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Rol__2A49584CD52AF031");

                entity.ToTable("Rol", "Seguridad");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.EsHistorico).HasDefaultValueSql("((0))");

                entity.Property(e => e.EstaActivo).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdUsuarioCreacionNavigation)
                    .WithMany(p => p.RolIdUsuarioCreacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rol_Creacion");

                entity.HasOne(d => d.IdUsuarioModificacionNavigation)
                    .WithMany(p => p.RolIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_Rol_Modificacion");
            });

            modelBuilder.Entity<RolCuenta>(entity =>
            {
                entity.HasKey(e => e.IdRolCuenta)
                    .HasName("PK__RolCuent__3706DEC9A4006052");

                entity.ToTable("RolCuenta", "Seguridad");

                entity.Property(e => e.EsHistorico).HasDefaultValueSql("((0))");

                entity.Property(e => e.EstaActivo).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.HasOne(d => d.IdCuentaNavigation)
                    .WithMany(p => p.RolCuentaIdCuentaNavigations)
                    .HasForeignKey(d => d.IdCuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolCuenta_Cuenta");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolCuenta)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolCuenta_Rol");

                entity.HasOne(d => d.IdUsuarioCreacionNavigation)
                    .WithMany(p => p.RolCuentaIdUsuarioCreacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolCuenta_Creacion");

                entity.HasOne(d => d.IdUsuarioModificacionNavigation)
                    .WithMany(p => p.RolCuentaIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_RolCuenta_Modificacion");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
