using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionApiTareas.Migrations
{
    /// <inheritdoc />
    public partial class GEST1003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "nota",
                table: "TaskEstudiantes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nota",
                table: "TaskEstudiantes");
        }
    }
}
