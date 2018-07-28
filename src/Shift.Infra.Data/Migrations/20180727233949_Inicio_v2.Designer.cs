﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shift.Infra.Data.Context;

namespace Shift.Infra.Data.Migrations
{
    [DbContext(typeof(ShiftContext))]
    [Migration("20180727233949_Inicio_v2")]
    partial class Inicio_v2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Shift.Domain.Cadastro.ModelsEstatica.TipoBloqueioModel.TipoBloqueio", b =>
                {
                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<bool>("Excluido");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Codigo");

                    b.ToTable("TipoBloqueio","Estatico");
                });
#pragma warning restore 612, 618
        }
    }
}
