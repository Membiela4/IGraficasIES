using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practica9.Migrations
{
    public partial class migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfesorFuncionario",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SeguroMedico = table.Column<int>(type: "int", nullable: false),
                    AnyoIngresoCuerpo = table.Column<int>(type: "int", nullable: false),
                    DestinoDefinitivo = table.Column<bool>(type: "bit", nullable: false),
                    EsFuncionario = table.Column<bool>(type: "bit", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    RutaFoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoProfesor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfesorFuncionario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfesorExtendido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfesorFuncionarioId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Peso = table.Column<int>(type: "int", nullable: false),
                    Estatura = table.Column<int>(type: "int", nullable: false),
                    EstadoCivilProfesor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfesorExtendido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfesorExtendido_ProfesorFuncionario_ProfesorFuncionarioId",
                        column: x => x.ProfesorFuncionarioId,
                        principalTable: "ProfesorFuncionario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfesorExtendido_ProfesorFuncionarioId",
                table: "ProfesorExtendido",
                column: "ProfesorFuncionarioId",
                unique: true,
                filter: "[ProfesorFuncionarioId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfesorExtendido");

            migrationBuilder.DropTable(
                name: "ProfesorFuncionario");
        }
    }
}
