using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class CriacaoTabelaEmpresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Cadastro");

            migrationBuilder.CreateTable(
                name: "Empresas",
                schema: "Cadastro",
                columns: table => new
                {
                    CodEmpresa = table.Column<string>(type: "varchar(04)", maxLength: 4, nullable: false),
                    IdSituacao = table.Column<int>(nullable: false),
                    Excluido = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    Cnpj = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.CodEmpresa);
                    table.ForeignKey(
                        name: "FK_Empresas_Situacao_IdSituacao",
                        column: x => x.IdSituacao,
                        principalSchema: "Estatico",
                        principalTable: "Situacao",
                        principalColumn: "IdSituacao",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_IdSituacao",
                schema: "Cadastro",
                table: "Empresas",
                column: "IdSituacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empresas",
                schema: "Cadastro");
        }
    }
}
