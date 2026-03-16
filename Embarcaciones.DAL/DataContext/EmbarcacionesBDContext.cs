using System;
using System.Collections.Generic;
using Embarcaciones.Models;
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

        public virtual DbSet<Accion> Accion { get; set; } = null!;
        public virtual DbSet<Catalogo> Catalogo { get; set; } = null!;
        public virtual DbSet<CatalogoValor> CatalogoValor { get; set; } = null!;
        public virtual DbSet<Cuenta> Cuenta { get; set; } = null!;
        public virtual DbSet<Departamento> Departamento { get; set; } = null!;
        public virtual DbSet<Embarcacion> Embarcacion { get; set; } = null!;
        public virtual DbSet<EmbarcacionConstruccion> EmbarcacionConstruccion { get; set; } = null!;
        public virtual DbSet<EmbarcacionPropietario> EmbarcacionPropietario { get; set; } = null!;
        public virtual DbSet<EmbarcacionPuertoSeleccion> EmbarcacionPuertoSeleccion { get; set; } = null!;
        public virtual DbSet<Municipio> Municipio { get; set; } = null!;
        public virtual DbSet<Pagina> Pagina { get; set; } = null!;
        public virtual DbSet<Persona> Persona { get; set; } = null!;
        public virtual DbSet<Rol> Rol { get; set; } = null!;
        public virtual DbSet<RolCuenta> RolCuenta { get; set; } = null!;
        public virtual DbSet<RolPaginaAccion> RolPaginaAccion { get; set; } = null!;
        public virtual DbSet<UnidadMedida> UnidadMedida { get; set; } = null!;

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
            modelBuilder.Entity<Accion>(entity =>
            {
                entity.HasKey(e => e.IdAccion)
                    .HasName("PK__Accion__9845169BC1539627");

                entity.ToTable("Accion", "Seguridad");

                entity.HasIndex(e => e.Nombre, "UQ_Accion_Nombre")
                    .IsUnique();

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.EstaActivo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.UsuarioCreacionNavigation)
                    .WithMany(p => p.AccionIdUsuarioCreacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accion_Creacion");

                entity.HasOne(d => d.UsuarioModificacionNavigation)
                    .WithMany(p => p.AccionIdUsuarioModificacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Accion_Modificacion");
            });

            modelBuilder.Entity<Catalogo>(entity =>
            {
                entity.HasKey(e => e.IdCatalogo)
                    .HasName("PK__Catalogo__FD0AC26C7F486B56");

                entity.ToTable("Catalogo", "Fundaciones");

                entity.HasIndex(e => e.CodigoInterno, "UQ_Catalogo_CodigoInterno")
                    .IsUnique();

                entity.HasIndex(e => e.Nombre, "UQ_Catalogo_Nombre")
                    .IsUnique();

                entity.Property(e => e.CodigoInterno).HasMaxLength(50);

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.EstaActivo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasOne(d => d.UsuarioCreacionNavigation)
                    .WithMany(p => p.CatalogoIdUsuarioCreacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Catalogo_Creacion");

                entity.HasOne(d => d.UsuarioModificacionNavigation)
                    .WithMany(p => p.CatalogoIdUsuarioModificacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Catalogo_Modificacion");
            });

            modelBuilder.Entity<CatalogoValor>(entity =>
            {
                entity.HasKey(e => e.IdCatalogoValor)
                    .HasName("PK__Catalogo__64C7006243FBA180");

                entity.ToTable("CatalogoValor", "Fundaciones");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.EstaActivo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasOne(d => d.IdCatalogoNavigation)
                    .WithMany(p => p.CatalogoValor)
                    .HasForeignKey(d => d.IdCatalogo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CatalogoValor_Catalogo");

                entity.HasOne(d => d.UsuarioCreacionNavigation)
                    .WithMany(p => p.CatalogoValorIdUsuarioCreacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CatalogoValor_Creacion");

                entity.HasOne(d => d.UsuarioModificacionNavigation)
                    .WithMany(p => p.CatalogoValorIdUsuarioModificacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_CatalogoValor_Modificacion");
            });

            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(e => e.IdCuenta)
                    .HasName("PK__Cuenta__D41FD70630A914B2");

                entity.ToTable("Cuenta", "Seguridad");

                entity.HasIndex(e => e.Usuario, "UQ_Cuenta_Usuario")
                    .IsUnique();

                entity.Property(e => e.ContrasenaHash).HasMaxLength(255);

                entity.Property(e => e.EstaActivo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Estado).HasMaxLength(20);

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Usuario).HasMaxLength(50);

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.IdPersona)
                    .HasConstraintName("FK_Cuenta_Persona");

                entity.HasOne(d => d.UsuarioCreacionNavigation)
                    .WithMany(p => p.InverseIdUsuarioCreacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .HasConstraintName("FK_Cuenta_Creacion");

                entity.HasOne(d => d.UsuarioModificacionNavigation)
                    .WithMany(p => p.InverseIdUsuarioModificacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_Cuenta_Modificacion");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.IdDepartamento)
                    .HasName("PK__Departam__787A433DA0B1B0F4");

                entity.ToTable("Departamento", "Fundaciones");

                entity.HasIndex(e => e.Departamento1, "UQ_Departamento_Nombre")
                    .IsUnique();

                entity.Property(e => e.Departamento1)
                    .HasMaxLength(100)
                    .HasColumnName("Departamento");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.EstaActivo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.HasOne(d => d.UsuarioCreacionNavigation)
                    .WithMany(p => p.DepartamentoIdUsuarioCreacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Departamento_Creacion");

                entity.HasOne(d => d.UsuarioModificacionNavigation)
                    .WithMany(p => p.DepartamentoIdUsuarioModificacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Departamento_Modificacion");
            });

            modelBuilder.Entity<Embarcacion>(entity =>
            {
                entity.HasKey(e => e.IdEmbarcacion)
                    .HasName("PK__Embarcac__D9F3219EA833EC5E");

                entity.ToTable("Embarcacion", "Embarcaciones");

                entity.Property(e => e.CapPce)
                    .HasMaxLength(50)
                    .HasColumnName("CAP_PCE");

                entity.Property(e => e.Distrito).HasMaxLength(50);

                entity.Property(e => e.EstaActivo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

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

                //entity.HasOne(d => d.IdActividadNavigation)
                //    .WithMany(p => p.EmbarcacionIdActividadNavigation)
                //    .HasForeignKey(d => d.IdActividad)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Embarcacion_Actividad");

                //entity.HasOne(d => d.IdBanderaRegistroActualNavigation)
                //    .WithMany(p => p.EmbarcacionIdBanderaRegistroActualNavigation)
                //    .HasForeignKey(d => d.IdBanderaRegistroActual)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Embarcacion_BanderaRegistroActual");

                //entity.HasOne(d => d.IdBanderaRegistroAnteriorNavigation)
                //    .WithMany(p => p.EmbarcacionIdBanderaRegistroAnteriorNavigation)
                //    .HasForeignKey(d => d.IdBanderaRegistroAnterior)
                //    .HasConstraintName("FK_Embarcacion_BanderaRegistroAnterior");

                //entity.HasOne(d => d.EmbarcacionConstruccionNavigation)
                //    .WithMany(p => p.Embarcacion)
                //    .HasForeignKey(d => d.IdEmbarcacionConstruccion)
                //    .HasConstraintName("FK_Embarcacion_Construccion");

                //entity.HasOne(d => d.EmbarcacionPropietarioNavigation)
                //    .WithMany(p => p.Embarcacion)
                //    .HasForeignKey(d => d.IdEmbarcacionPropietario)
                //    .HasConstraintName("FK_Embarcacion_Propietario");

                //entity.HasOne(d => d.IdPuertoRegistroActualNavigation)
                //    .WithMany(p => p.EmbarcacionIdPuertoRegistroActualNavigation)
                //    .HasForeignKey(d => d.IdPuertoRegistroActual)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Embarcacion_PuertoRegistroActual");

                //entity.HasOne(d => d.IdPuertoRegistroAnteriorNavigation)
                //    .WithMany(p => p.EmbarcacionIdPuertoRegistroAnteriorNavigation)
                //    .HasForeignKey(d => d.IdPuertoRegistroAnterior)
                //    .HasConstraintName("FK_Embarcacion_PuertoRegistroAnterior");

                //entity.HasOne(d => d.IdTipoEmbarcacionNavigation)
                //    .WithMany(p => p.EmbarcacionIdTipoEmbarcacionNavigation)
                //    .HasForeignKey(d => d.IdTipoEmbarcacion)
                //    .HasConstraintName("FK_Embarcacion_TipoEmbarcacion");

                entity.HasOne(d => d.UsuarioCreacionNavigation)
                    .WithMany(p => p.EmbarcacionIdUsuarioCreacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Embarcacion_Creacion");

                entity.HasOne(d => d.UsuarioModificacionNavigation)
                    .WithMany(p => p.EmbarcacionIdUsuarioModificacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Embarcacion_Modificacion");

                //entity.HasOne(d => d.IdZonaNavegacionNavigation)
                //    .WithMany(p => p.EmbarcacionIdZonaNavegacionNavigation)
                //    .HasForeignKey(d => d.IdZonaNavegacion)
                //    .HasConstraintName("FK_Embarcacion_ZonaNavegacion");
            });

            modelBuilder.Entity<EmbarcacionConstruccion>(entity =>
            {
                entity.HasKey(e => e.IdEmbarcacionConstruccion)
                    .HasName("PK__Embarcac__189A72FDBF1470C9");

                entity.ToTable("EmbarcacionConstruccion", "Embarcaciones");

                entity.Property(e => e.Calado).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CapacidadCarga).HasMaxLength(255);

                entity.Property(e => e.Eslora).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.EstaActivo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Frecuencia).HasMaxLength(255);

                //entity.Property(e => e.IdUnidadMedidaCalado).HasColumnName("IdUnidadMedida_Calado");

                //entity.Property(e => e.IdUnidadMedidaEslora).HasColumnName("IdUnidadMedida_Eslora");

                //entity.Property(e => e.IdUnidadMedidaManga).HasColumnName("IdUnidadMedida_Manga");

                //entity.Property(e => e.IdUnidadMedidaPuntal).HasColumnName("IdUnidadMedida_Puntal");

                //entity.Property(e => e.IdUnidadMedidaTrb).HasColumnName("IdUnidadMedida_TRB");

                //entity.Property(e => e.IdUnidadMedidaTrn).HasColumnName("IdUnidadMedida_TRN");

                entity.Property(e => e.Indicativo).HasMaxLength(50);

                entity.Property(e => e.Manga).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.MediosCx)
                    .HasMaxLength(255)
                    .HasColumnName("MediosCX");

                entity.Property(e => e.ModeloMotor).HasMaxLength(255);

                entity.Property(e => e.NumeroConstruccion).HasMaxLength(255);

                entity.Property(e => e.Potencia).HasMaxLength(255);

                entity.Property(e => e.Puntal).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.SerieMotor).HasMaxLength(255);

                entity.Property(e => e.Trb)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("TRB");

                entity.Property(e => e.Trn)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("TRN");

                //entity.HasOne(d => d.IdColorObraMuertaNavigation)
                //    .WithMany(p => p.EmbarcacionConstruccionIdColorObraMuertaNavigation)
                //    .HasForeignKey(d => d.IdColorObraMuerta)
                //    .HasConstraintName("FK_EmbConst_ColorObraMuerta");

                //entity.HasOne(d => d.IdColorObraVivaNavigation)
                //    .WithMany(p => p.EmbarcacionConstruccionIdColorObraVivaNavigation)
                //    .HasForeignKey(d => d.IdColorObraViva)
                //    .HasConstraintName("FK_EmbConst_ColorObraViva");

                //entity.HasOne(d => d.IdColorSuperestructuraNavigation)
                //    .WithMany(p => p.EmbarcacionConstruccionIdColorSuperestructuraNavigation)
                //    .HasForeignKey(d => d.IdColorSuperestructura)
                //    .HasConstraintName("FK_EmbConst_ColorSuperestructura");

                //entity.HasOne(d => d.IdMarcaMotorNavigation)
                //    .WithMany(p => p.EmbarcacionConstruccionIdMarcaMotorNavigation)
                //    .HasForeignKey(d => d.IdMarcaMotor)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_EmbConst_MarcaMotor");

                //entity.HasOne(d => d.IdPropulsionNavigation)
                //    .WithMany(p => p.EmbarcacionConstruccionIdPropulsionNavigation)
                //    .HasForeignKey(d => d.IdPropulsion)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_EmbConst_Propulsion");

                //entity.HasOne(d => d.IdSistemaNavegacionNavigation)
                //    .WithMany(p => p.EmbarcacionConstruccionIdSistemaNavegacionNavigation)
                //    .HasForeignKey(d => d.IdSistemaNavegacion)
                //    .HasConstraintName("FK_EmbConst_SistemaNavegacion");

                //entity.HasOne(d => d.IdTipoComunicacionNavigation)
                //    .WithMany(p => p.EmbarcacionConstruccionIdTipoComunicacionNavigation)
                //    .HasForeignKey(d => d.IdTipoComunicacion)
                //    .HasConstraintName("FK_EmbConst_TipoComunicacion");

                //entity.HasOne(d => d.IdUnidadMedidaCaladoNavigation)
                //    .WithMany(p => p.EmbarcacionConstruccionIdUnidadMedidaCaladoNavigation)
                //    .HasForeignKey(d => d.IdUnidadMedidaCalado)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_EmbConst_UM_Calado");

                //entity.HasOne(d => d.IdUnidadMedidaEsloraNavigation)
                //    .WithMany(p => p.EmbarcacionConstruccionIdUnidadMedidaEsloraNavigation)
                //    .HasForeignKey(d => d.IdUnidadMedidaEslora)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_EmbConst_UM_Eslora");

                //entity.HasOne(d => d.IdUnidadMedidaMangaNavigation)
                //    .WithMany(p => p.EmbarcacionConstruccionIdUnidadMedidaMangaNavigation)
                //    .HasForeignKey(d => d.IdUnidadMedidaManga)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_EmbConst_UM_Manga");

                //entity.HasOne(d => d.IdUnidadMedidaPuntalNavigation)
                //    .WithMany(p => p.EmbarcacionConstruccionIdUnidadMedidaPuntalNavigation)
                //    .HasForeignKey(d => d.IdUnidadMedidaPuntal)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_EmbConst_UM_Puntal");

                //entity.HasOne(d => d.IdUnidadMedidaTrbNavigation)
                //    .WithMany(p => p.EmbarcacionConstruccionIdUnidadMedidaTrbNavigation)
                //    .HasForeignKey(d => d.IdUnidadMedidaTrb)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_EmbConst_UM_TRB");

                //entity.HasOne(d => d.IdUnidadMedidaTrnNavigation)
                //    .WithMany(p => p.EmbarcacionConstruccionIdUnidadMedidaTrnNavigation)
                //    .HasForeignKey(d => d.IdUnidadMedidaTrn)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_EmbConst_UM_TRN");

                entity.HasOne(d => d.UsuarioCreacionNavigation)
                    .WithMany(p => p.EmbarcacionConstruccionIdUsuarioCreacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmbConst_Creacion");

                entity.HasOne(d => d.UsuarioModificacionNavigation)
                    .WithMany(p => p.EmbarcacionConstruccionIdUsuarioModificacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_EmbConst_Modificacion");
            });

            modelBuilder.Entity<EmbarcacionPropietario>(entity =>
            {
                entity.HasKey(e => e.IdEmbarcacionPropietario)
                    .HasName("PK__Embarcac__9A7F72A552FF262E");

                entity.ToTable("EmbarcacionPropietario", "Embarcaciones");

                entity.Property(e => e.Domicilio).HasMaxLength(255);

                entity.Property(e => e.EmpresaPropietaria).HasMaxLength(255);

                entity.Property(e => e.EsHistorico).HasDefaultValueSql("((0))");

                entity.Property(e => e.EstaActivo).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LicenciaNavegacion).HasMaxLength(255);

                entity.Property(e => e.NombreContacto).HasMaxLength(255);

                entity.Property(e => e.NombrePropietario).HasMaxLength(150);

                entity.Property(e => e.NumeroCarnetMarinero).HasMaxLength(255);

                entity.Property(e => e.RutaImagenPropietario).HasMaxLength(255);

                entity.Property(e => e.Telefono).HasMaxLength(30);

                entity.Property(e => e.TelefonoEmpresaPropietaria).HasMaxLength(255);

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.EmbarcacionPropietario)
                    .HasForeignKey(d => d.IdDepartamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmbarcacionPropietario_Departamento");

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.EmbarcacionPropietario)
                    .HasForeignKey(d => d.IdMunicipio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmbarcacionPropietario_Municipio");

                //entity.HasOne(d => d.IdNacionalidadNavigation)
                //    .WithMany(p => p.EmbarcacionPropietario)
                //    .HasForeignKey(d => d.IdNacionalidad)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_EmbarcacionPropietario_Nacionalidad");

                entity.HasOne(d => d.UsuarioCreacionNavigation)
                    .WithMany(p => p.EmbarcacionPropietarioIdUsuarioCreacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmbarcacionPropietario_Creacion");

                entity.HasOne(d => d.UsuarioModificacionNavigation)
                    .WithMany(p => p.EmbarcacionPropietarioIdUsuarioModificacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_EmbarcacionPropietario_Modificacion");
            });

            modelBuilder.Entity<EmbarcacionPuertoSeleccion>(entity =>
            {
                entity.HasKey(e => e.IdEmbarcacionPuertoSeleccion)
                    .HasName("PK__Embarcac__701B111262147527");

                entity.ToTable("EmbarcacionPuertoSeleccion", "Embarcaciones");

                entity.Property(e => e.EstaActivo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.HasOne(d => d.IdEmbarcacionConstruccionNavigation)
                    .WithMany(p => p.EmbarcacionPuertoSeleccion)
                    .HasForeignKey(d => d.IdEmbarcacionConstruccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmbarcacionPuertoSeleccion_Embarcacion");

                entity.HasOne(d => d.UsuarioCreacionNavigation)
                    .WithMany(p => p.EmbarcacionPuertoSeleccionIdUsuarioCreacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmbarcacionPuertoSeleccion_Creacion");

                entity.HasOne(d => d.UsuarioModificacionNavigation)
                    .WithMany(p => p.EmbarcacionPuertoSeleccionIdUsuarioModificacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_EmbarcacionPuertoSeleccion_Modificacion");
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.HasKey(e => e.IdMunicipio)
                    .HasName("PK__Municipi__61005978DF95848F");

                entity.ToTable("Municipio", "Fundaciones");

                entity.HasIndex(e => new { e.IdDepartamento, e.Municipio1 }, "UQ_Municipio_Departamento_Nombre")
                    .IsUnique();

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.EstaActivo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Municipio1)
                    .HasMaxLength(100)
                    .HasColumnName("Municipio");

                entity.HasOne(d => d.DepartamentoNavigation)
                    .WithMany(p => p.Municipio)
                    .HasForeignKey(d => d.IdDepartamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Municipio_Departamento");

                entity.HasOne(d => d.UsuarioCreacionNavigation)
                    .WithMany(p => p.MunicipioIdUsuarioCreacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Municipio_Creacion");

                entity.HasOne(d => d.UsuarioModificacionNavigation)
                    .WithMany(p => p.MunicipioIdUsuarioModificacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Municipio_Modificacion");
            });

            modelBuilder.Entity<Pagina>(entity =>
            {
                entity.HasKey(e => e.IdPagina)
                    .HasName("PK__Pagina__034F88B8D4D6D4CC");

                entity.ToTable("Pagina", "Seguridad");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.EsHistorico).HasDefaultValueSql("((0))");

                entity.Property(e => e.EstaActivo).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.UrlPagina).HasMaxLength(255);

                entity.HasOne(d => d.UsuarioCreacionNavigation)
                    .WithMany(p => p.PaginaIdUsuarioCreacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pagina_Creacion");

                entity.HasOne(d => d.UsuarioModificacionNavigation)
                    .WithMany(p => p.PaginaIdUsuarioModificacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Pagina_Modificacion");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona)
                    .HasName("PK__Persona__2EC8D2AC30A775C2");

                entity.ToTable("Persona", "Fundaciones");

                entity.Property(e => e.Correo).HasMaxLength(100);

                entity.Property(e => e.Direccion).HasMaxLength(255);

                entity.Property(e => e.EstaActivo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Identificacion).HasMaxLength(50);

                entity.Property(e => e.NombreCompleto).HasMaxLength(150);

                entity.Property(e => e.Telefono).HasMaxLength(30);

                entity.HasOne(d => d.TipoIdentificacionNavigation)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdTipoIdentificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Persona_TipoIdentificacion");

                entity.HasOne(d => d.UsuarioCreacionNavigation)
                    .WithMany(p => p.PersonaIdUsuarioCreacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Persona_Creacion");

                entity.HasOne(d => d.UsuarioModificacionNavigation)
                    .WithMany(p => p.PersonaIdUsuarioModificacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Persona_Modificacion");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Rol__2A49584C777C90B0");

                entity.ToTable("Rol", "Seguridad");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.EstaActivo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.UsuarioCreacionNavigation)
                    .WithMany(p => p.RolIdUsuarioCreacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rol_Creacion");

                entity.HasOne(d => d.UsuarioModificacionNavigation)
                    .WithMany(p => p.RolIdUsuarioModificacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Rol_Modificacion");
            });

            modelBuilder.Entity<RolCuenta>(entity =>
            {
                entity.HasKey(e => e.IdRolCuenta)
                    .HasName("PK__RolCuent__3706DEC9F9D7E769");

                entity.ToTable("RolCuenta", "Seguridad");

                entity.HasIndex(e => new { e.IdCuenta, e.IdRol }, "UQ_RolCuenta")
                    .IsUnique();

                entity.Property(e => e.EstaActivo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.HasOne(d => d.IdCuentaNavigation)
                    .WithMany(p => p.RolCuentaIdCuentaNavigation)
                    .HasForeignKey(d => d.IdCuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolCuenta_Cuenta");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolCuenta)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolCuenta_Rol");

                entity.HasOne(d => d.UsuarioCreacionNavigation)
                    .WithMany(p => p.RolCuentaIdUsuarioCreacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolCuenta_Creacion");

                entity.HasOne(d => d.UsuarioModificacionNavigation)
                    .WithMany(p => p.RolCuentaIdUsuarioModificacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_RolCuenta_Modificacion");
            });

            modelBuilder.Entity<RolPaginaAccion>(entity =>
            {
                entity.HasKey(e => e.IdRolPaginaAccion)
                    .HasName("PK__RolPagin__8E7D504226819560");

                entity.ToTable("RolPaginaAccion", "Seguridad");

                entity.HasIndex(e => new { e.IdRol, e.IdPagina, e.IdAccion }, "UQ_RolPaginaAccion")
                    .IsUnique();

                entity.Property(e => e.EstaActivo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.EstaPermitido)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.HasOne(d => d.IdAccionNavigation)
                    .WithMany(p => p.RolPaginaAccion)
                    .HasForeignKey(d => d.IdAccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RPA_Accion");

                entity.HasOne(d => d.IdPaginaNavigation)
                    .WithMany(p => p.RolPaginaAccion)
                    .HasForeignKey(d => d.IdPagina)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RPA_Pagina");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolPaginaAccion)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RPA_Rol");

                entity.HasOne(d => d.UsuarioCreacionNavigation)
                    .WithMany(p => p.RolPaginaAccionIdUsuarioCreacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RPA_Creacion");

                entity.HasOne(d => d.UsuarioModificacionNavigation)
                    .WithMany(p => p.RolPaginaAccionIdUsuarioModificacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_RPA_Modificacion");
            });

            modelBuilder.Entity<UnidadMedida>(entity =>
            {
                entity.HasKey(e => e.IdUnidadMedida)
                    .HasName("PK__UnidadMe__18F83A9317DB8D1F");

                entity.ToTable("UnidadMedida", "Fundaciones");

                entity.Property(e => e.Abreviatura).HasMaxLength(20);

                entity.Property(e => e.Descripcion).HasMaxLength(200);

                entity.Property(e => e.EstaActivo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasOne(d => d.UsuarioCreacionNavigation)
                    .WithMany(p => p.UnidadMedidaIdUsuarioCreacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnidadMedida_Creacion");

                entity.HasOne(d => d.UsuarioModificacionNavigation)
                    .WithMany(p => p.UnidadMedidaIdUsuarioModificacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_UnidadMedida_Modificacion");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
