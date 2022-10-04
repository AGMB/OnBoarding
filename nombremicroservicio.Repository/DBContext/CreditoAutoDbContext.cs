using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using nombremicroservicio.Entities.Entidades;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace nombremicroservicio.Repository.DBContext
{
    public partial class CreditoAutoDbContext : DbContext
    {
        public CreditoAutoDbContext()
        {
        }

        public CreditoAutoDbContext(DbContextOptions<CreditoAutoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<ClientePatio> ClientePatio { get; set; }
        public virtual DbSet<Ejecutivo> Ejecutivo { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<Patio> Patio { get; set; }
        public virtual DbSet<SolicitudCredito> SolicitudCredito { get; set; }
        public virtual DbSet<Vehiculo> Vehiculo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoCivil)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Identificacion)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.IdentificacionConyuge)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.NombreConyuge)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClientePatio>(entity =>
            {
                entity.Property(e => e.ClientePatioId).HasColumnName("ClientePatioID");

                entity.Property(e => e.FechaAsignacion).HasColumnType("datetime");

                entity.Property(e => e.IdentificacionCliente)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.PatioId).HasColumnName("PatioID");
            });

            modelBuilder.Entity<Ejecutivo>(entity =>
            {
                entity.Property(e => e.EjecutivoId).HasColumnName("EjecutivoID");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Identificacion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PatioId).HasColumnName("PatioID");

                entity.Property(e => e.TelefonoConvencional)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.Property(e => e.MarcaId).HasColumnName("MarcaID");

                entity.Property(e => e.NombreMarca)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Patio>(entity =>
            {
                entity.Property(e => e.PatioId).HasColumnName("PatioID");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SolicitudCredito>(entity =>
            {
                entity.HasKey(e => e.SolicitudId);

                entity.Property(e => e.SolicitudId).HasColumnName("SolicitudID");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.Cuotas).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.EjecutivoId).HasColumnName("EjecutivoID");

                entity.Property(e => e.Entrada).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FechaSolicitud).HasColumnType("date");

                entity.Property(e => e.EstadoSolicitud)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observacion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PatioId).HasColumnName("PatioID");

                entity.Property(e => e.VehiculoId).HasColumnName("VehiculoID");
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.Property(e => e.VehiculoId).HasColumnName("VehiculoID");

                entity.Property(e => e.Avaluo).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Cilindraje)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MarcaId).HasColumnName("MarcaID");

                entity.Property(e => e.Modelo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroChasis)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Placa)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
