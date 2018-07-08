using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.CrossCutting.Identity.Migrations
{
    public partial class AjusteCampoMatricula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Matricula",
                table: "AspNetUsers",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Matricula",
                table: "AspNetUsers",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 6);
        }
    }
}
