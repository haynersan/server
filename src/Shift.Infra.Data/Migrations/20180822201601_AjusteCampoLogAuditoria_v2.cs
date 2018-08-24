using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class AjusteCampoLogAuditoria_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeUsuario",
                schema: "Cadastro",
                table: "LogAuditorias");

            migrationBuilder.AddColumn<Guid>(
                name: "UserIdLogado",
                schema: "Cadastro",
                table: "LogAuditorias",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserIdLogado",
                schema: "Cadastro",
                table: "LogAuditorias");

            migrationBuilder.AddColumn<string>(
                name: "NomeUsuario",
                schema: "Cadastro",
                table: "LogAuditorias",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");
        }
    }
}
