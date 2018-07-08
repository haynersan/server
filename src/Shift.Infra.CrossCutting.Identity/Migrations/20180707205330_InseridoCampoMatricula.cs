using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.CrossCutting.Identity.Migrations
{
    public partial class InseridoCampoMatricula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Matricula",
                table: "AspNetUsers",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Matricula",
                table: "AspNetUsers");
        }
    }
}
