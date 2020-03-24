using Hilfswerk.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hilfswerk.EntityFramework
{
    public class HilfswerkDbContext : DbContext
    {
        public DbSet<Helfer> Helfer { get; set; }
        public DbSet<Einsatz> Einsaetze { get; set; }

        public HilfswerkDbContext(DbContextOptions<HilfswerkDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Helfer>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.OwnsOne(p => p.Kontakt);
                entity.Property(p => p.Anmerkung)
                    .HasMaxLength(2000);
                entity.HasMany(p => p.Einsaetze)
                    .WithOne(p => p.Helfer)
                    .IsRequired();
            });
            builder.Entity<HelferTaetigkeit>(entity =>
            {
                entity.HasKey(p => new { p.HelferId, p.TaetigkeitId });
                entity.HasOne(p => p.Taetigkeit)
                    .WithMany(p => p.HelferTaetigkeiten)
                    .HasForeignKey(p => p.TaetigkeitId);
                entity.HasOne(p => p.Helfer)
                    .WithMany(p => p.HelferTaetigkeiten)
                    .HasForeignKey(p => p.HelferId);
            });

            builder.Entity<Einsatz>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.HasOne(p => p.Taetigkeit)
                    .WithMany(p => p.Einsaetze)
                    .IsRequired();
                entity.Property(p => p.VermitteltDurch)
                    .HasMaxLength(200)
                    .IsRequired();
                entity.Property(p => p.Hilfesuchender)
                    .HasMaxLength(200)
                    .IsRequired();
                entity.Property(p => p.Anmerkungen)
                    .HasMaxLength(2000);
            });
            builder.Entity<Taetigkeit>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id)
                    .ValueGeneratedOnAdd();
                entity.Property(p => p.Label)
                    .HasMaxLength(50);
                entity.HasData(Taetigkeit.TELEFON_KONTAKT, Taetigkeit.GASSI_GEHEN, Taetigkeit.BESORGUNG, Taetigkeit.ANDERE);
            });



        }

    }
}
