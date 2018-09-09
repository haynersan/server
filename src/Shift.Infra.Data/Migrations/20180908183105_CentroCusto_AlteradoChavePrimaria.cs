using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class CentroCusto_AlteradoChavePrimaria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CentroCustos",
                schema: "Cadastro",
                table: "CentroCustos");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Cadastro",
                table: "CentroCustos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CentroCustos",
                schema: "Cadastro",
                table: "CentroCustos",
                column: "CodCentroCusto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CentroCustos",
                schema: "Cadastro",
                table: "CentroCustos");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "Cadastro",
                table: "CentroCustos",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CentroCustos",
                schema: "Cadastro",
                table: "CentroCustos",
                column: "Id");
        }
    }
}
