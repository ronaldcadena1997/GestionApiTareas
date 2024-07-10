using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionApiTareas.Migrations
{
    /// <inheritdoc />
    public partial class GEST1005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EFQMs",
                columns: table => new
                {
                    idEFQM = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Leadership = table.Column<int>(type: "int", nullable: false),
                    Strategy = table.Column<int>(type: "int", nullable: false),
                    People = table.Column<int>(type: "int", nullable: false),
                    Partnerships = table.Column<int>(type: "int", nullable: false),
                    Processes = table.Column<int>(type: "int", nullable: false),
                    ResultsCustomer = table.Column<int>(type: "int", nullable: false),
                    ResultsPeople = table.Column<int>(type: "int", nullable: false),
                    ResultsSociety = table.Column<int>(type: "int", nullable: false),
                    ResultsKey = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EFQMs", x => x.idEFQM);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EFQMs");
        }
    }
}
