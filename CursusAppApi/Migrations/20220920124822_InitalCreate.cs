using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursusApp.Backend.Migrations
{
    public partial class InitalCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cursussen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Duur = table.Column<int>(type: "int", nullable: false),
                    Titel = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursussen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CursusInstanties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Startdatum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CursusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursusInstanties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CursusInstanties_Cursussen_CursusId",
                        column: x => x.CursusId,
                        principalTable: "Cursussen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CursusInstanties_CursusId",
                table: "CursusInstanties",
                column: "CursusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CursusInstanties");

            migrationBuilder.DropTable(
                name: "Cursussen");
        }
    }
}
