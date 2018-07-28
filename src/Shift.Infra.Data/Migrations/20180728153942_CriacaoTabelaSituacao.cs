using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class CriacaoTabelaSituacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Situacao",
                schema: "Estatico",
                columns: table => new
                {
                    IdSituacao = table.Column<int>(nullable: false),
                    Excluido = table.Column<bool>(nullable: false),
                    DescSituacao = table.Column<string>(type: "varchar(20)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false, defaultValue: new DateTime(2018, 7, 28, 0, 0, 0, 0, DateTimeKind.Local))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Situacao", x => x.IdSituacao);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Situacao",
                schema: "Estatico");
        }
    }
}
