using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeportNetReconocimiento.Migrations
{
    /// <inheritdoc />
    public partial class corregirForeingKeyVentas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ventas_socios_branch_member_id",
                table: "ventas");

            migrationBuilder.DropIndex(
                name: "IX_ventas_branch_member_id",
                table: "ventas");

            migrationBuilder.AddColumn<int>(
                name: "branchMemberId",
                table: "ventas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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
    }
}
