using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.Data.Migrations
{
    public partial class CriacaoTabelaCentroCusto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CentroCustos",
                schema: "Cadastro",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CodEmpresa = table.Column<string>(nullable: false),
                    IdSituacao = table.Column<int>(nullable: false),
                    Excluido = table.Column<bool>(nullable: false),
                    CodCentroCusto = table.Column<long>(type: "bigint", nullable: false),
                    NomeCentroCusto = table.Column<string>(type: "varchar(200)", nullable: false),
                    OrigemLegado = table.Column<bool>(type: "bit", nullable: false),
                    CodGrupo = table.Column<string>(nullable: false),
                    CodClasse = table.Column<int>(nullable: false),
                    CodTipoBloqueio = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentroCustos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentroCustos_ClasseContabeis_CodClasse",
                        column: x => x.CodClasse,
                        principalSchema: "Estatico",
                        principalTable: "ClasseContabeis",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CentroCustos_Empresas_CodEmpresa",
                        column: x => x.CodEmpresa,
                        principalSchema: "Cadastro",
                        principalTable: "Empresas",
                        principalColumn: "CodEmpresa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CentroCustos_Grupos_CodGrupo",
                        column: x => x.CodGrupo,
                        principalSchema: "Estatico",
                        principalTable: "Grupos",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CentroCustos_TipoBloqueios_CodTipoBloqueio",
                        column: x => x.CodTipoBloqueio,
                        principalSchema: "Estatico",
                        principalTable: "TipoBloqueios",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CentroCustos_CodClasse",
                schema: "Cadastro",
                table: "CentroCustos",
                column: "CodClasse");

            migrationBuilder.CreateIndex(
                name: "IX_CentroCustos_CodEmpresa",
                schema: "Cadastro",
                table: "CentroCustos",
                column: "CodEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_CentroCustos_CodGrupo",
                schema: "Cadastro",
                table: "CentroCustos",
                column: "CodGrupo");

            migrationBuilder.CreateIndex(
                name: "IX_CentroCustos_CodTipoBloqueio",
                schema: "Cadastro",
                table: "CentroCustos",
                column: "CodTipoBloqueio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CentroCustos",
                schema: "Cadastro");
        }
    }
}
