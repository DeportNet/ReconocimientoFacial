using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeportNetReconocimiento.Migrations
{
    /// <inheritdoc />
    public partial class atributoCompanyMemberIdCredenciales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "current_company_member_id",
                table: "credenciales",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "current_company_member_id",
                table: "credenciales");
        }
    }
}
