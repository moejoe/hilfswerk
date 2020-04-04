﻿// <auto-generated />
using System;
using Hilfswerk.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hilfswerk.Api.Migrations
{
    [DbContext(typeof(HilfswerkDbContext))]
    partial class HilfswerkDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("Hilfswerk.EntityFramework.Entities.Einsatz", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Anmerkungen")
                        .HasColumnType("TEXT")
                        .HasMaxLength(2000);

                    b.Property<string>("HelferId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Hilfesuchender")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<int>("TaetigkeitId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("VermitteltAm")
                        .HasColumnType("TEXT");

                    b.Property<string>("VermitteltDurch")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("HelferId");

                    b.HasIndex("TaetigkeitId");

                    b.ToTable("Einsaetze");
                });

            modelBuilder.Entity("Hilfswerk.EntityFramework.Entities.Helfer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Anmerkung")
                        .HasColumnType("TEXT")
                        .HasMaxLength(2000);

                    b.Property<bool>("hatAuto")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("istAusgelastet")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("istFreiwilliger")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("istRisikogrupepe")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("istZivildiener")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Helfer");
                });

            modelBuilder.Entity("Hilfswerk.EntityFramework.Entities.HelferTaetigkeit", b =>
                {
                    b.Property<string>("HelferId")
                        .HasColumnType("TEXT");

                    b.Property<int>("TaetigkeitId")
                        .HasColumnType("INTEGER");

                    b.HasKey("HelferId", "TaetigkeitId");

                    b.HasIndex("TaetigkeitId");

                    b.ToTable("HelferTaetigkeit");
                });

            modelBuilder.Entity("Hilfswerk.EntityFramework.Entities.Taetigkeit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Label")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Taetigkeit");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Label = "Telefonkontakt"
                        },
                        new
                        {
                            Id = 4,
                            Label = "Gassi gehen"
                        },
                        new
                        {
                            Id = 1,
                            Label = "Besorgung"
                        },
                        new
                        {
                            Id = 8,
                            Label = "Andere"
                        });
                });

            modelBuilder.Entity("Hilfswerk.EntityFramework.Entities.Einsatz", b =>
                {
                    b.HasOne("Hilfswerk.EntityFramework.Entities.Helfer", "Helfer")
                        .WithMany("Einsaetze")
                        .HasForeignKey("HelferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hilfswerk.EntityFramework.Entities.Taetigkeit", "Taetigkeit")
                        .WithMany("Einsaetze")
                        .HasForeignKey("TaetigkeitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hilfswerk.EntityFramework.Entities.Helfer", b =>
                {
                    b.OwnsOne("Hilfswerk.EntityFramework.Entities.Kontakt", "Kontakt", b1 =>
                        {
                            b1.Property<string>("HelferId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Email")
                                .HasColumnType("TEXT");

                            b1.Property<string>("GeoLocation")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Nachname")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Plz")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Strasse")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Telefon")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Vorname")
                                .HasColumnType("TEXT");

                            b1.HasKey("HelferId");

                            b1.ToTable("Helfer");

                            b1.WithOwner()
                                .HasForeignKey("HelferId");
                        });
                });

            modelBuilder.Entity("Hilfswerk.EntityFramework.Entities.HelferTaetigkeit", b =>
                {
                    b.HasOne("Hilfswerk.EntityFramework.Entities.Helfer", "Helfer")
                        .WithMany("HelferTaetigkeiten")
                        .HasForeignKey("HelferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hilfswerk.EntityFramework.Entities.Taetigkeit", "Taetigkeit")
                        .WithMany("HelferTaetigkeiten")
                        .HasForeignKey("TaetigkeitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
