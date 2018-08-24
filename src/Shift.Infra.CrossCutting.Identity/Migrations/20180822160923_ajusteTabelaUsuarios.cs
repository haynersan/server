using Microsoft.EntityFrameworkCore.Migrations;

namespace Shift.Infra.CrossCutting.Identity.Migrations
{
    public partial class ajusteTabelaUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Matricula",
                table: "AspNetUsers",
                maxLength: 6,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 6);

            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Matricula",
                table: "AspNetUsers",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 6,
                oldNullable: true);
        }
    }
}
