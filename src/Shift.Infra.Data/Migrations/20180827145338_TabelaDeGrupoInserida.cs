using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class TabelaDeGrupoInserida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                schema: "Estatico",
                table: "Situacoes",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(2018, 8, 27, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValue: new DateTime(2018, 8, 23, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateTable(
                name: "Grupos",
                schema: "Estatico",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "varchar(03)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.Codigo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grupos",
                schema: "Estatico");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                schema: "Estatico",
                table: "Situacoes",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(2018, 8, 23, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValue: new DateTime(2018, 8, 27, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
