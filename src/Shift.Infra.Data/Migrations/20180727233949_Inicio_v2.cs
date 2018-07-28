using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class Inicio_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Estatico");

            migrationBuilder.RenameTable(
                name: "TipoBloqueio",
                schema: "Cadastro",
                newName: "TipoBloqueio",
                newSchema: "Estatico");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Cadastro");

            migrationBuilder.RenameTable(
                name: "TipoBloqueio",
                schema: "Estatico",
                newName: "TipoBloqueio",
                newSchema: "Cadastro");
        }
    }
}
