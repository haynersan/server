using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class CentroCusto_NovaChavePrimaria_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos",
                type: "varchar(04)",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(04)",
                oldMaxLength: 4);
        }
    }
}
