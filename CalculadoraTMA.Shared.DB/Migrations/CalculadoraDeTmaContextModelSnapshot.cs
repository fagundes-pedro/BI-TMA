﻿// <auto-generated />
using System;
using BI_TMA.Shared.DB.Banco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Calculadora_de_TMA.Migrations
{
    [DbContext(typeof(BI_TMAContext))]
    partial class CalculadoraDeTmaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Calculadora_de_TMA.Modelos.Assistente", b =>
                {
                    b.Property<int>("AssistenteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AssistenteId"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AssistenteId");

                    b.ToTable("Assistentes");
                });

            modelBuilder.Entity("Calculadora_de_TMA.Modelos.Chamada", b =>
                {
                    b.Property<Guid>("ChamadaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AssistenteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("datetime2");

                    b.Property<int>("LinhaId")
                        .HasColumnType("int");

                    b.Property<double>("TempoDeChamada")
                        .HasColumnType("float");

                    b.HasKey("ChamadaId");

                    b.HasIndex("AssistenteId");

                    b.HasIndex("LinhaId");

                    b.ToTable("Chamadas");
                });

            modelBuilder.Entity("Calculadora_de_TMA.Modelos.Linha", b =>
                {
                    b.Property<int>("LinhaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LinhaId"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LinhaId");

                    b.ToTable("Linhas");
                });

            modelBuilder.Entity("Calculadora_de_TMA.Modelos.LinhaAssistente", b =>
                {
                    b.Property<int>("AssistenteId")
                        .HasColumnType("int");

                    b.Property<int>("LinhaId")
                        .HasColumnType("int");

                    b.Property<double>("TMA")
                        .HasColumnType("float");

                    b.HasKey("AssistenteId", "LinhaId");

                    b.HasIndex("LinhaId");

                    b.ToTable("LinhasAssistentes");
                });

            modelBuilder.Entity("Calculadora_de_TMA.Modelos.Chamada", b =>
                {
                    b.HasOne("Calculadora_de_TMA.Modelos.Assistente", "Assistente")
                        .WithMany("Chamadas")
                        .HasForeignKey("AssistenteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Calculadora_de_TMA.Modelos.Linha", "Linha")
                        .WithMany("Chamadas")
                        .HasForeignKey("LinhaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Assistente");

                    b.Navigation("Linha");
                });

            modelBuilder.Entity("Calculadora_de_TMA.Modelos.LinhaAssistente", b =>
                {
                    b.HasOne("Calculadora_de_TMA.Modelos.Assistente", "Assistente")
                        .WithMany("LinhasAssistentes")
                        .HasForeignKey("AssistenteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Calculadora_de_TMA.Modelos.Linha", "Linha")
                        .WithMany("LinhasAssistentes")
                        .HasForeignKey("LinhaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assistente");

                    b.Navigation("Linha");
                });

            modelBuilder.Entity("Calculadora_de_TMA.Modelos.Assistente", b =>
                {
                    b.Navigation("Chamadas");

                    b.Navigation("LinhasAssistentes");
                });

            modelBuilder.Entity("Calculadora_de_TMA.Modelos.Linha", b =>
                {
                    b.Navigation("Chamadas");

                    b.Navigation("LinhasAssistentes");
                });
#pragma warning restore 612, 618
        }
    }
}
