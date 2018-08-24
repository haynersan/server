using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class AjusteNomeLogAuditoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LogAuditoria",
                schema: "Cadastro",
                table: "LogAuditoria");

            migrationBuilder.RenameTable(
                name: "LogAuditoria",
                schema: "Cadastro",
                newName: "LogAuditorias",
                newSchema: "Cadastro");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LogAuditorias",
                schema: "Cadastro",
                table: "LogAuditorias",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LogAuditorias",
                schema: "Cadastro",
                table: "LogAuditorias");

            migrationBuilder.RenameTable(
                name: "LogAuditorias",
                schema: "Cadastro",
                newName: "LogAuditoria",
                newSchema: "Cadastro");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LogAuditoria",
                schema: "Cadastro",
                table: "LogAuditoria",
                column: "Id");
        }
    }
}
