using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practica9.Migrations
{
    public partial class migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Materia",
                table: "ProfesorFuncionario",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Materia",
                table: "ProfesorFuncionario");
        }
    }
}
