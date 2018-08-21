using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class AjusteTabelaEmpresa_V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cnpj",
                schema: "Cadastro",
                table: "Empresas",
                type: "varchar(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cnpj",
                schema: "Cadastro",
                table: "Empresas");
        }
    }
}
