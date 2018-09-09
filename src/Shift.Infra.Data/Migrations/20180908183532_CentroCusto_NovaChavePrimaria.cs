using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class CentroCusto_NovaChavePrimaria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CentroCustos_CodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CentroCustos_CodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos",
                column: "CodEmpresa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_CentroCustos_CodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos");

            migrationBuilder.CreateIndex(
                name: "IX_CentroCustos_CodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos",
                column: "CodEmpresa");
        }
    }
}
