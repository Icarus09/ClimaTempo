using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CidadeClima.Models
{
    public partial class ClimaTempoSimplesContext : DbContext
    {
        public ClimaTempoSimplesContext()
        {
        }

        public ClimaTempoSimplesContext(DbContextOptions<ClimaTempoSimplesContext> options)
            : base(options)
        {
        }

        public  DbSet<Cidade> Cidade { get; set; }
        public  DbSet<Estado> Estado { get; set; }
        public  DbSet<PrevisaoClima> PrevisaoClima { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=ClimaTempoSimples;user id=sa;password=1q2w3e4r@#$;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cidade>(entity =>
            {

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Estado>(entity =>
            {

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Uf)
                    .IsRequired()
                    .HasColumnName("UF")
                    .HasMaxLength(2);
            });

            modelBuilder.Entity<PrevisaoClima>(entity =>
            {
                entity.Property(e => e.Clima)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.DataPrevisao).HasColumnType("date");

                entity.Property(e => e.TemperaturaMaxima).HasColumnType("numeric(3, 1)");

                entity.Property(e => e.TemperaturaMinima).HasColumnType("numeric(3, 1)");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
