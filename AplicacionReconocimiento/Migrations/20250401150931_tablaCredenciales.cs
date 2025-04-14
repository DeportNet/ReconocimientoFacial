using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeportNetReconocimiento.Migrations
{
    /// <inheritdoc />
    public partial class tablaCredenciales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "credenciales",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ip = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true),
                    port = table.Column<string>(type: "TEXT", maxLength: 5, nullable: true),
                    username = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    branch_id = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    branch_token = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_credenciales", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "credenciales");
        }
    }
}
