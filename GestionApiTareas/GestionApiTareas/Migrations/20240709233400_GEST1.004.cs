using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionApiTareas.Migrations
{
    /// <inheritdoc />
    public partial class GEST1004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "estadoNota",
                table: "TaskEstudiantes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "estadoNota",
                table: "TaskEstudiantes");
        }
    }
}
