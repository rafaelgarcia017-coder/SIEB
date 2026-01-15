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

        public virtual DbSet<Cuenta> Cuenta { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<RolCuenta> RolCuenta { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {    
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(e => e.IdCuenta)
                    .HasName("PK__Cuenta__D41FD7068854787F");

                entity.HasIndex(e => e.IdPersona, "IX_Cuenta_IdPersona");

                entity.HasIndex(e => e.Nombre, "UQ_Cuenta_Nombre")
                    .IsUnique();

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion).HasMaxLength(150);

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.PasswordHash).HasMaxLength(255);

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cuenta_Persona");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona)
                    .HasName("PK__Persona__2EC8D2AC5C6B7170");

                entity.ToTable("Persona");

                entity.HasIndex(e => e.Correo, "IX_Persona_Correo")
                    .IsUnique();

                entity.Property(e => e.Correo).HasMaxLength(100);

                entity.Property(e => e.Direccion).HasMaxLength(200);

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Identificacion).HasMaxLength(50);

                entity.Property(e => e.NombreCompleto).HasMaxLength(150);

                entity.Property(e => e.Telefono).HasMaxLength(20);
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Rol__2A49584C200FF672");

                entity.ToTable("Rol");

                entity.HasIndex(e => e.CodigoInterno, "UQ__Rol__28C92875F1B812C4")
                    .IsUnique();

                entity.Property(e => e.CodigoInterno).HasMaxLength(20);

                entity.Property(e => e.Descripcion).HasMaxLength(150);

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<RolCuenta>(entity =>
            {
                entity.HasKey(e => e.IdRolCuenta)
                    .HasName("PK__RolCuent__3706DEC960D924B6");

                entity.HasIndex(e => e.IdCuenta, "IX_RolCuenta_IdCuenta");

                entity.HasIndex(e => new { e.IdCuenta, e.IdRol }, "UQ_RolCuenta")
                    .IsUnique();

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

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
