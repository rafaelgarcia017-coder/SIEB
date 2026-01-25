using System;
using Embarcaciones.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
                    .HasName("PK__Catalogo__FD0AC26CB9AE892B");

                entity.ToTable("Catalogo", "Fundaciones");

                entity.Property(e => e.CodigoInterno).HasMaxLength(50);

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<CatalogoValor>(entity =>
            {
                entity.HasKey(e => e.IdCatalogoValor)
                    .HasName("PK__Catalogo__64C70062344CBCF4");

                entity.ToTable("CatalogoValor", "Fundaciones");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasOne(d => d.IdCatalogoNavigation)
                    .WithMany(p => p.CatalogoValors)
                    .HasForeignKey(d => d.IdCatalogo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CatalogoValor_Catalogo");
            });

            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(e => e.IdCuenta)
                    .HasName("PK__Cuenta__D41FD70694B58EC3");

                entity.ToTable("Cuenta", "Seguridad");

                entity.HasIndex(e => e.Usuario, "UQ__Cuenta__E3237CF707248F7D")
                    .IsUnique();

                entity.Property(e => e.ContrasenaHash).HasMaxLength(255);

                entity.Property(e => e.EsHistorico).HasDefaultValueSql("((0))");

                entity.Property(e => e.EstaActivo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Estado).HasMaxLength(20);

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Usuario).HasMaxLength(50);
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.IdDepartamento)
                    .HasName("PK__Departam__787A433D489AE93B");

                entity.ToTable("Departamento", "Fundaciones");

                entity.Property(e => e.Departamento1)
                    .HasMaxLength(100)
                    .HasColumnName("Departamento");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.EsHistorico).HasDefaultValueSql("((0))");

                entity.Property(e => e.EstaActivo).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");
            });

            modelBuilder.Entity<Embarcacion>(entity =>
            {
                entity.HasKey(e => e.IdEmbarcacion)
                    .HasName("PK__Embarcac__D9F3219E557FE120");

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
                    .WithMany(p => p.EmbarcacionIdEmbarcacionConstruccionNavigations)
                    .HasForeignKey(d => d.IdEmbarcacionConstruccion)
                    .HasConstraintName("FK_Embarcacion_EmbConst");

                entity.HasOne(d => d.IdEmbarcacionPropietarioNavigation)
                    .WithMany(p => p.EmbarcacionIdEmbarcacionPropietarioNavigations)
                    .HasForeignKey(d => d.IdEmbarcacionPropietario)
                    .HasConstraintName("FK_Embarcacion_EmbProp");
            });

            modelBuilder.Entity<EmbarcacionConstruccion>(entity =>
            {
                entity.HasKey(e => e.IdEmbarcacionConstruccion)
                    .HasName("PK__Embarcac__189A72FD120A32C4");

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
            });

            modelBuilder.Entity<EmbarcacionPropietario>(entity =>
            {
                entity.HasKey(e => e.IdEmbarcacionPropietario)
                    .HasName("PK__Embarcac__9A7F72A55FDCE731");

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
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.HasKey(e => e.IdMunicipio)
                    .HasName("PK__Municipi__61005978195C470C");

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
            });

            modelBuilder.Entity<Pagina>(entity =>
            {
                entity.HasKey(e => e.IdPagina)
                    .HasName("PK__Pagina__034F88B8D15163ED");

                entity.ToTable("Pagina", "Seguridad");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.EsHistorico).HasDefaultValueSql("((0))");

                entity.Property(e => e.EstaActivo).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.UrlPagina).HasMaxLength(255);
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona)
                    .HasName("PK__Persona__2EC8D2ACCCCB5FB0");

                entity.ToTable("Persona", "Fundaciones");

                entity.Property(e => e.Correo).HasMaxLength(100);

                entity.Property(e => e.Direccion).HasMaxLength(255);

                entity.Property(e => e.EsHistorico).HasDefaultValueSql("((0))");

                entity.Property(e => e.EstaActivo).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Identificacion).HasMaxLength(50);

                entity.Property(e => e.NombreCompleto).HasMaxLength(150);

                entity.Property(e => e.Telefono).HasMaxLength(30);

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.IdMunicipio)
                    .HasConstraintName("FK_Persona_Municipio");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Rol__2A49584CDEB0AA73");

                entity.ToTable("Rol", "Seguridad");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.EsHistorico).HasDefaultValueSql("((0))");

                entity.Property(e => e.EstaActivo).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<RolCuenta>(entity =>
            {
                entity.HasKey(e => e.IdRolCuenta)
                    .HasName("PK__RolCuent__3706DEC9CE71A1B4");

                entity.ToTable("RolCuenta", "Seguridad");

                entity.Property(e => e.EsHistorico).HasDefaultValueSql("((0))");

                entity.Property(e => e.EstaActivo).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.HasOne(d => d.IdCuentaNavigation)
                    .WithMany(p => p.RolCuenta)
                    .HasForeignKey(d => d.IdCuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolCuenta_Cuenta");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolCuenta)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolCuenta_Rol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
