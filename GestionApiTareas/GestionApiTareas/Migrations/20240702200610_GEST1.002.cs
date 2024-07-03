using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionApiTareas.Migrations
{
    /// <inheritdoc />
    public partial class GEST1002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskEstudiantes",
                columns: table => new
                {
                    idTask = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEstudiante = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    nombreEstuiante = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SesionRegistro = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    SesionModificacion = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    SesionEliminacion = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    IpRegistro = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    IpModificacion = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    IpEliminacion = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    UsuarioRegistro = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    UsuarioEliminacion = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    SistemaEliminacion = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    SistemaRegistro = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    SistemaModificacion = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEstudiantes", x => x.idTask);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskEstudiantes");
        }
    }
}
