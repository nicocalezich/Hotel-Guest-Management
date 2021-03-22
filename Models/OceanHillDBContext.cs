using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProyectoHotelNT1.Models
{
    public partial class OceanHillDBContext : DbContext
    {
        public OceanHillDBContext()
        {
        }

        public OceanHillDBContext(DbContextOptions<OceanHillDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Habitaciones> Habitaciones { get; set; }
        public virtual DbSet<Huespedes> Huespedes { get; set; }
        public virtual DbSet<Registro> Registro { get; set; }
        public virtual DbSet<Credenciales> Credenciales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=NULLPOINTEREXCE\\SQLEXPRESS;Database=OceanHillDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Habitaciones>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CapacidadMax).HasColumnName("Capacidad_max");

                entity.Property(e => e.EstaOcupada).HasColumnName("Esta_ocupada");

                entity.Property(e => e.NroHabitacion).HasColumnName("Nro_habitacion");

                entity.Property(e => e.PrecioPorDia).HasColumnName("Precio_por_dia");
            });

            modelBuilder.Entity<Huespedes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DiasHospedado).HasColumnName("Dias_hospedado");

                entity.Property(e => e.Dni).HasColumnName("DNI");

                entity.Property(e => e.FechaEgreso)
                    .HasColumnName("Fecha_egreso")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnName("Fecha_ingreso")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NroHabitacion).HasColumnName("Nro_habitacion");
            });

            modelBuilder.Entity<Registro>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.DiasHospedado).HasColumnName("Dias_hospedado");

                entity.Property(e => e.Dni).HasColumnName("DNI");

                entity.Property(e => e.FechaEgreso)
                    .HasColumnName("Fecha_egreso")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnName("Fecha_ingreso")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.NroHabitacion).HasColumnName("Nro_habitacion");

                entity.Property(e => e.IdActivo).HasColumnName("Id_activo");
            });

            
            modelBuilder.Entity<Credenciales>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");            

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });
            
        }
    }
}
