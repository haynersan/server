using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class CentroCusto_NovaChavePrimaria_v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentroCustos_Empresas_EmpresasCodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CentroCustos",
                schema: "Cadastro",
                table: "CentroCustos");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_CentroCustos_CodEmpresa",
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

            migrationBuilder.AlterColumn<string>(
                name: "CodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(04)",
                oldMaxLength: 4);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CentroCustos",
                schema: "Cadastro",
                table: "CentroCustos",
                columns: new[] { "CodEmpresa", "CodCentroCusto" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentroCustos_Empresas_CodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CentroCustos",
                schema: "Cadastro",
                table: "CentroCustos");

            migrationBuilder.AlterColumn<string>(
                name: "CodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos",
                type: "varchar(04)",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "EmpresasCodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CentroCustos",
                schema: "Cadastro",
                table: "CentroCustos",
                column: "CodCentroCusto");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CentroCustos_CodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos",
                column: "CodEmpresa");

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
    }
}
