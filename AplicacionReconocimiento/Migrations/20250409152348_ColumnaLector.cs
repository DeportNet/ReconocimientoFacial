using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeportNetReconocimiento.Migrations
{
    /// <inheritdoc />
    public partial class ColumnaLector : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "face_reader",
                table: "configuracion_general",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "face_reader",
                table: "configuracion_general");
        }
    }
}
