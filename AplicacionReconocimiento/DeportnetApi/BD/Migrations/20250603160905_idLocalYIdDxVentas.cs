using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeportNetReconocimiento.Migrations
{
    /// <inheritdoc />
    public partial class idLocalYIdDxVentas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ventas_socios_branch_member_id",
                table: "ventas");

            migrationBuilder.DropIndex(
                name: "IX_ventas_branch_member_id",
                table: "ventas");

            migrationBuilder.AddColumn<int>(
                name: "local_member_id",
                table: "ventas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ventas_local_member_id",
                table: "ventas",
                column: "local_member_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ventas_socios_local_member_id",
                table: "ventas",
                column: "local_member_id",
                principalTable: "socios",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ventas_socios_local_member_id",
                table: "ventas");

            migrationBuilder.DropIndex(
                name: "IX_ventas_local_member_id",
                table: "ventas");

            migrationBuilder.DropColumn(
                name: "local_member_id",
                table: "ventas");

            migrationBuilder.CreateIndex(
                name: "IX_ventas_branch_member_id",
                table: "ventas",
                column: "branch_member_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ventas_socios_branch_member_id",
                table: "ventas",
                column: "branch_member_id",
                principalTable: "socios",
                principalColumn: "id");
        }
    }
}
