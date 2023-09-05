﻿// <auto-generated />
using System;
using ControleLancamentos.Infraestrutura.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ControleLancamentos.Migrations
{
    [DbContext(typeof(CaixaContext))]
    partial class CaixaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ControleLancamentos.Dominio.AgregadorRaiz.Caixa", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("DataDeAbertura")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataDeFechamento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("SituacaoDoCaixa")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Caixa", (string)null);
                });

            modelBuilder.Entity("ControleLancamentos.Dominio.Entidades.Lancamento", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CaixaId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DataDeRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TipoDoLancamento")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CaixaId");

                    b.ToTable("Lancamento", (string)null);
                });

            modelBuilder.Entity("ControleLancamentos.Dominio.Entidades.Lancamento", b =>
                {
                    b.HasOne("ControleLancamentos.Dominio.AgregadorRaiz.Caixa", "Caixa")
                        .WithMany("Lancamentos")
                        .HasForeignKey("CaixaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Caixa");
                });

            modelBuilder.Entity("ControleLancamentos.Dominio.AgregadorRaiz.Caixa", b =>
                {
                    b.Navigation("Lancamentos");
                });
#pragma warning restore 612, 618
        }
    }
}