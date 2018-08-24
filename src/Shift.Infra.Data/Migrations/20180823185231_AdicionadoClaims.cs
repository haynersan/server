using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class AdicionadoClaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                schema: "Estatico",
                table: "Situacoes",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(2018, 8, 23, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValue: new DateTime(2018, 8, 22, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateTable(
                name: "ClaimValues",
                schema: "Estatico",
                columns: table => new
                {
                    Excluido = table.Column<bool>(nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimValues", x => x.Codigo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimValues",
                schema: "Estatico");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                schema: "Estatico",
                table: "Situacoes",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(2018, 8, 22, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValue: new DateTime(2018, 8, 23, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
