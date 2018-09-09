using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class AjusteTabelaSituacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCadastro",
                schema: "Estatico",
                table: "Situacoes");

            migrationBuilder.DropColumn(
                name: "Excluido",
                schema: "Estatico",
                table: "Situacoes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                schema: "Estatico",
                table: "Situacoes",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(2018, 8, 27, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                schema: "Estatico",
                table: "Situacoes",
                nullable: false,
                defaultValue: false);
        }
    }
}
