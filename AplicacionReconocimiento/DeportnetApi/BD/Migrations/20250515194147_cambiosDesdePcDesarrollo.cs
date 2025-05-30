using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeportNetReconocimiento.Migrations
{
    /// <inheritdoc />
    public partial class cambiosDesdePcDesarrollo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "syncronized_date",
                table: "ventas",
                newName: "synchronized_date");

            migrationBuilder.AddColumn<int>(
                name: "branchMemberId",
                table: "ventas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "sale_amount",
                table: "ventas",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "sale_name",
                table: "ventas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ventas_branchMemberId",
                table: "ventas",
                column: "branchMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_ventas_socios_branchMemberId",
                table: "ventas",
                column: "branchMemberId",
                principalTable: "socios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ventas_socios_branchMemberId",
                table: "ventas");

            migrationBuilder.DropIndex(
                name: "IX_ventas_branchMemberId",
                table: "ventas");

            migrationBuilder.DropColumn(
                name: "branchMemberId",
                table: "ventas");

            migrationBuilder.DropColumn(
                name: "sale_amount",
                table: "ventas");

            migrationBuilder.DropColumn(
                name: "sale_name",
                table: "ventas");

            migrationBuilder.RenameColumn(
                name: "synchronized_date",
                table: "ventas",
                newName: "syncronized_date");
        }
    }
}
