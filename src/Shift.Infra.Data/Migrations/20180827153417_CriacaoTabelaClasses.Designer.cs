﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shift.Infra.Data.Context;

namespace Shift.Infra.Data.Migrations
{
    [DbContext(typeof(ShiftContext))]
    [Migration("20180827153417_CriacaoTabelaClasses")]
    partial class CriacaoTabelaClasses
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Shift.Domain.Cadastro.EmpresaModel.Empresa", b =>
                {
                    b.Property<string>("CodEmpresa")
                        .HasColumnType("varchar(04)")
                        .HasMaxLength(4);

                    b.Property<bool>("Excluido");

                    b.Property<int>("IdSituacao");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("CodEmpresa");

                    b.HasIndex("IdSituacao");

                    b.ToTable("Empresas","Cadastro");
                });

            modelBuilder.Entity("Shift.Domain.Cadastro.LogAuditoriaModel.LogAuditoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Acao")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime>("DataOperacao")
                        .HasColumnType("datetime");

                    b.Property<string>("JsonResult")
                        .IsRequired()
                        .HasColumnType("varchar(4000)");

                    b.Property<string>("Modulo")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Schema")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Tabela")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("UserIdLogado")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("LogAuditorias","Cadastro");
                });

            modelBuilder.Entity("Shift.Domain.Cadastro.ModelsEstatica.ClaimModel.ClaimValue", b =>
                {
                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<bool>("Excluido");

                    b.Property<string>("Valor")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Codigo");

                    b.ToTable("ClaimValues","Estatico");
                });

            modelBuilder.Entity("Shift.Domain.Cadastro.ModelsEstatica.ClasseModel.Classe", b =>
                {
                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Codigo");

                    b.ToTable("Classes","Estatico");
                });

            modelBuilder.Entity("Shift.Domain.Cadastro.ModelsEstatica.GrupoModel.Grupo", b =>
                {
                    b.Property<string>("Codigo")
                        .HasColumnType("varchar(03)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Codigo");

                    b.ToTable("Grupos","Estatico");
                });

            modelBuilder.Entity("Shift.Domain.Cadastro.ModelsEstatica.SituacaoModel.Situacao", b =>
                {
                    b.Property<int>("IdSituacao");

                    b.Property<string>("DescSituacao")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("IdSituacao");

                    b.ToTable("Situacoes","Estatico");
                });

            modelBuilder.Entity("Shift.Domain.Cadastro.ModelsEstatica.TipoBloqueioModel.TipoBloqueio", b =>
                {
                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Codigo");

                    b.ToTable("TipoBloqueios","Estatico");
                });

            modelBuilder.Entity("Shift.Domain.Cadastro.EmpresaModel.Empresa", b =>
                {
                    b.HasOne("Shift.Domain.Cadastro.ModelsEstatica.SituacaoModel.Situacao", "Situacao")
                        .WithMany("Empresas")
                        .HasForeignKey("IdSituacao");

                    b.OwnsOne("Shift.Domain.Core.ValueObjects.CNPJ", "CNPJ", b1 =>
                        {
                            b1.Property<string>("EmpresaCodEmpresa");

                            b1.Property<string>("NumeroCNPJ")
                                .IsRequired()
                                .HasColumnName("Cnpj")
                                .HasColumnType("varchar(14)")
                                .HasMaxLength(14);

                            b1.ToTable("Empresas","Cadastro");

                            b1.HasOne("Shift.Domain.Cadastro.EmpresaModel.Empresa")
                                .WithOne("CNPJ")
                                .HasForeignKey("Shift.Domain.Core.ValueObjects.CNPJ", "EmpresaCodEmpresa")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
