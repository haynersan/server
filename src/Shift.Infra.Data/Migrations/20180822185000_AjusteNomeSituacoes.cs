using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class AjusteNomeSituacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_Situacao_IdSituacao",
                schema: "Cadastro",
                table: "Empresas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Situacao",
                schema: "Estatico",
                table: "Situacao");

            migrationBuilder.RenameTable(
                name: "Situacao",
                schema: "Estatico",
                newName: "Situacoes",
                newSchema: "Estatico");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Situacoes",
                schema: "Estatico",
                table: "Situacoes",
                column: "IdSituacao");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_Situacoes_IdSituacao",
                schema: "Cadastro",
                table: "Empresas",
                column: "IdSituacao",
                principalSchema: "Estatico",
                principalTable: "Situacoes",
                principalColumn: "IdSituacao",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_Situacoes_IdSituacao",
                schema: "Cadastro",
                table: "Empresas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Situacoes",
                schema: "Estatico",
                table: "Situacoes");

            migrationBuilder.RenameTable(
                name: "Situacoes",
                schema: "Estatico",
                newName: "Situacao",
                newSchema: "Estatico");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Situacao",
                schema: "Estatico",
                table: "Situacao",
                column: "IdSituacao");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_Situacao_IdSituacao",
                schema: "Cadastro",
                table: "Empresas",
                column: "IdSituacao",
                principalSchema: "Estatico",
                principalTable: "Situacao",
                principalColumn: "IdSituacao",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
