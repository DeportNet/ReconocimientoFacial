using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeportNetReconocimiento.Migrations
{
    /// <inheritdoc />
    public partial class migraciondeFabri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "empleados",
                newName: "full_name");

            migrationBuilder.AlterColumn<string>(
                name: "full_name",
                table: "empleados",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "current_face_quantity",
                table: "configuracion_general",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "max_face_quantity",
                table: "configuracion_general",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "current_face_quantity",
                table: "configuracion_general");

            migrationBuilder.DropColumn(
                name: "max_face_quantity",
                table: "configuracion_general");

            migrationBuilder.RenameColumn(
                name: "full_name",
                table: "empleados",
                newName: "FullName");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "empleados",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
