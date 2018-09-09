using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class CentroCusto_NovaChavePrimaria_v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentroCustos_Empresas_CodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos");

            migrationBuilder.AddColumn<string>(
                name: "EmpresasCodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CentroCustos_EmpresasCodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos",
                column: "EmpresasCodEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_CentroCustos_Empresas_EmpresasCodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos",
                column: "EmpresasCodEmpresa",
                principalSchema: "Cadastro",
                principalTable: "Empresas",
                principalColumn: "CodEmpresa",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentroCustos_Empresas_EmpresasCodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos");

            migrationBuilder.DropIndex(
                name: "IX_CentroCustos_EmpresasCodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos");

            migrationBuilder.DropColumn(
                name: "EmpresasCodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos");

            migrationBuilder.AddForeignKey(
                name: "FK_CentroCustos_Empresas_CodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos",
                column: "CodEmpresa",
                principalSchema: "Cadastro",
                principalTable: "Empresas",
                principalColumn: "CodEmpresa",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
