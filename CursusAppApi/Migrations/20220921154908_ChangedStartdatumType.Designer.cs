﻿// <auto-generated />
using System;
using CursusApp.Backend.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CursusApp.Backend.Migrations
{
    [DbContext(typeof(CursusDbContext))]
    [Migration("20220921154908_ChangedStartdatumType")]
    partial class ChangedStartdatumType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CursusApp.Core.Models.Cursus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("Duur")
                        .HasColumnType("int");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("Cursussen");
                });

            modelBuilder.Entity("CursusApp.Core.Models.CursusInstantie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CursusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Startdatum")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CursusId");

                    b.ToTable("CursusInstanties");
                });

            modelBuilder.Entity("CursusApp.Core.Models.CursusInstantie", b =>
                {
                    b.HasOne("CursusApp.Core.Models.Cursus", "Cursus")
                        .WithMany("CursusInstanties")
                        .HasForeignKey("CursusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cursus");
                });

            modelBuilder.Entity("CursusApp.Core.Models.Cursus", b =>
                {
                    b.Navigation("CursusInstanties");
                });
#pragma warning restore 612, 618
        }
    }
}
