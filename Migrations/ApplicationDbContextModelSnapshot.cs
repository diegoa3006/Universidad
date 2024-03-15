﻿// <auto-generated />
using MiApiUniversidad.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MiApiUniversidad.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MiApiUniversidad.Modelos.Departamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departamentos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descripcion = "Facultad Derecho",
                            Nombre = "Derecho"
                        },
                        new
                        {
                            Id = 2,
                            Descripcion = "San Camilo",
                            Nombre = "Trabajo Social"
                        });
                });

            modelBuilder.Entity("MiApiUniversidad.Modelos.Materia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Creditos")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Materias");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Creditos = 3,
                            Nombre = "Creditos 1"
                        },
                        new
                        {
                            Id = 2,
                            Creditos = 4,
                            Nombre = "Creditos 2"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
