using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class CriacaoTabelaCentroCusto_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdSituacao",
                schema: "Cadastro",
                table: "CentroCustos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdSituacao",
                schema: "Cadastro",
                table: "CentroCustos",
                nullable: false,
                defaultValue: 0);
        }
    }
}
