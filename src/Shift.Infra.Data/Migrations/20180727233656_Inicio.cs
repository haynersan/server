using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class Inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Cadastro");

            migrationBuilder.CreateTable(
                name: "TipoBloqueio",
                schema: "Cadastro",
                columns: table => new
                {
                    Excluido = table.Column<bool>(nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoBloqueio", x => x.Codigo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoBloqueio",
                schema: "Cadastro");
        }
    }
}
