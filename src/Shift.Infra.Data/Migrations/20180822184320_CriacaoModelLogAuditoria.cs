using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class CriacaoModelLogAuditoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                schema: "Estatico",
                table: "Situacao",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(2018, 8, 22, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValue: new DateTime(2018, 7, 28, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateTable(
                name: "LogAuditoria",
                schema: "Cadastro",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataOperacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    Schema = table.Column<string>(type: "varchar(50)", nullable: false),
                    Tabela = table.Column<string>(type: "varchar(100)", nullable: false),
                    Acao = table.Column<string>(type: "varchar(10)", nullable: false),
                    Modulo = table.Column<string>(type: "varchar(200)", nullable: false),
                    JsonResult = table.Column<string>(type: "varchar(4000)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogAuditoria", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogAuditoria",
                schema: "Cadastro");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                schema: "Estatico",
                table: "Situacao",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(2018, 7, 28, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValue: new DateTime(2018, 8, 22, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
