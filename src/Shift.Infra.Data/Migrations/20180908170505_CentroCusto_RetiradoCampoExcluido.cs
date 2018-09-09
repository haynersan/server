using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class CentroCusto_RetiradoCampoExcluido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Excluido",
                schema: "Cadastro",
                table: "CentroCustos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                schema: "Cadastro",
                table: "CentroCustos",
                nullable: false,
                defaultValue: false);
        }
    }
}
