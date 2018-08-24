using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class AjusteNomeTipoBloqueio_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoBloqueio",
                schema: "Estatico",
                table: "TipoBloqueio");

            migrationBuilder.RenameTable(
                name: "TipoBloqueio",
                schema: "Estatico",
                newName: "TipoBloqueios",
                newSchema: "Estatico");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoBloqueios",
                schema: "Estatico",
                table: "TipoBloqueios",
                column: "Codigo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoBloqueios",
                schema: "Estatico",
                table: "TipoBloqueios");

            migrationBuilder.RenameTable(
                name: "TipoBloqueios",
                schema: "Estatico",
                newName: "TipoBloqueio",
                newSchema: "Estatico");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoBloqueio",
                schema: "Estatico",
                table: "TipoBloqueio",
                column: "Codigo");
        }
    }
}
