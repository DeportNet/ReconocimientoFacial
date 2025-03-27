using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeportNetReconocimiento.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNotationsAndCredentials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accesos_socios_accesos_AccesoId",
                table: "accesos_socios");

            migrationBuilder.RenameColumn(
                name: "Period",
                table: "membresias",
                newName: "period");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "membresias",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "IsSaleItem",
                table: "membresias",
                newName: "isSaleItem");

            migrationBuilder.RenameColumn(
                name: "Days",
                table: "membresias",
                newName: "days");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "membresias",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "empleados",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "empleados",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "empleados",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "empleados",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "CompanyMemberId",
                table: "empleados",
                newName: "company_member_id");

            migrationBuilder.RenameColumn(
                name: "UltimaFechaSincronizacion",
                table: "configuracion_general",
                newName: "last_syncro");

            migrationBuilder.RenameColumn(
                name: "NombreSucursal",
                table: "configuracion_general",
                newName: "branch_name");

            migrationBuilder.RenameColumn(
                name: "ContraseniaBd",
                table: "configuracion_general",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "CantMaxLotes",
                table: "configuracion_general",
                newName: "max_lot_quantity");

            migrationBuilder.RenameColumn(
                name: "AnteriorFechaSincronizacion",
                table: "configuracion_general",
                newName: "prior_last_syncro");

            migrationBuilder.RenameColumn(
                name: "StartCharacter",
                table: "configuracion_de_acceso",
                newName: "startCharacter");

            migrationBuilder.RenameColumn(
                name: "SecondStartCharacter",
                table: "configuracion_de_acceso",
                newName: "secondStartCharacter");

            migrationBuilder.RenameColumn(
                name: "EndCharacter",
                table: "configuracion_de_acceso",
                newName: "endCharacter");

            migrationBuilder.RenameColumn(
                name: "CardLength",
                table: "configuracion_de_acceso",
                newName: "cardLength");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "articulos",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "IsSaleItem",
                table: "articulos",
                newName: "isSaleItem");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "articulos",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "IsSuccessful",
                table: "accesos_socios",
                newName: "isSuccessful");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "accesos_socios",
                newName: "member_id");

            migrationBuilder.RenameColumn(
                name: "CompanyMemberId",
                table: "accesos_socios",
                newName: "company_member_id");

            migrationBuilder.RenameColumn(
                name: "AccessDate",
                table: "accesos_socios",
                newName: "access_date");

            migrationBuilder.RenameColumn(
                name: "AccesoId",
                table: "accesos_socios",
                newName: "access_id");

            migrationBuilder.RenameIndex(
                name: "IX_accesos_socios_AccesoId",
                table: "accesos_socios",
                newName: "IX_accesos_socios_access_id");

            migrationBuilder.RenameColumn(
                name: "ProcessId",
                table: "accesos",
                newName: "process_id");

            migrationBuilder.RenameColumn(
                name: "ActiveBranchId",
                table: "accesos",
                newName: "active_branch_id");

            migrationBuilder.AlterColumn<int>(
                name: "period",
                table: "membresias",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "days",
                table: "membresias",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<double>(
                name: "amount",
                table: "membresias",
                type: "REAL",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "startCharacter",
                table: "configuracion_de_acceso",
                type: "TEXT",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "secondStartCharacter",
                table: "configuracion_de_acceso",
                type: "TEXT",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "endCharacter",
                table: "configuracion_de_acceso",
                type: "TEXT",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "cardLength",
                table: "configuracion_de_acceso",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<double>(
                name: "amount",
                table: "articulos",
                type: "REAL",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<bool>(
                name: "isSuccessful",
                table: "accesos_socios",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "member_id",
                table: "accesos_socios",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "company_member_id",
                table: "accesos_socios",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_accesos_socios_accesos_access_id",
                table: "accesos_socios",
                column: "access_id",
                principalTable: "accesos",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accesos_socios_accesos_access_id",
                table: "accesos_socios");

            migrationBuilder.RenameColumn(
                name: "period",
                table: "membresias",
                newName: "Period");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "membresias",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "isSaleItem",
                table: "membresias",
                newName: "IsSaleItem");

            migrationBuilder.RenameColumn(
                name: "days",
                table: "membresias",
                newName: "Days");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "membresias",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "empleados",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "empleados",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "empleados",
                newName: "isActive");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "empleados",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "company_member_id",
                table: "empleados",
                newName: "CompanyMemberId");

            migrationBuilder.RenameColumn(
                name: "prior_last_syncro",
                table: "configuracion_general",
                newName: "AnteriorFechaSincronizacion");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "configuracion_general",
                newName: "ContraseniaBd");

            migrationBuilder.RenameColumn(
                name: "max_lot_quantity",
                table: "configuracion_general",
                newName: "CantMaxLotes");

            migrationBuilder.RenameColumn(
                name: "last_syncro",
                table: "configuracion_general",
                newName: "UltimaFechaSincronizacion");

            migrationBuilder.RenameColumn(
                name: "branch_name",
                table: "configuracion_general",
                newName: "NombreSucursal");

            migrationBuilder.RenameColumn(
                name: "startCharacter",
                table: "configuracion_de_acceso",
                newName: "StartCharacter");

            migrationBuilder.RenameColumn(
                name: "secondStartCharacter",
                table: "configuracion_de_acceso",
                newName: "SecondStartCharacter");

            migrationBuilder.RenameColumn(
                name: "endCharacter",
                table: "configuracion_de_acceso",
                newName: "EndCharacter");

            migrationBuilder.RenameColumn(
                name: "cardLength",
                table: "configuracion_de_acceso",
                newName: "CardLength");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "articulos",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "isSaleItem",
                table: "articulos",
                newName: "IsSaleItem");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "articulos",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "isSuccessful",
                table: "accesos_socios",
                newName: "IsSuccessful");

            migrationBuilder.RenameColumn(
                name: "member_id",
                table: "accesos_socios",
                newName: "MemberId");

            migrationBuilder.RenameColumn(
                name: "company_member_id",
                table: "accesos_socios",
                newName: "CompanyMemberId");

            migrationBuilder.RenameColumn(
                name: "access_id",
                table: "accesos_socios",
                newName: "AccesoId");

            migrationBuilder.RenameColumn(
                name: "access_date",
                table: "accesos_socios",
                newName: "AccessDate");

            migrationBuilder.RenameIndex(
                name: "IX_accesos_socios_access_id",
                table: "accesos_socios",
                newName: "IX_accesos_socios_AccesoId");

            migrationBuilder.RenameColumn(
                name: "process_id",
                table: "accesos",
                newName: "ProcessId");

            migrationBuilder.RenameColumn(
                name: "active_branch_id",
                table: "accesos",
                newName: "ActiveBranchId");

            migrationBuilder.AlterColumn<string>(
                name: "Period",
                table: "membresias",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Days",
                table: "membresias",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Amount",
                table: "membresias",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "StartCharacter",
                table: "configuracion_de_acceso",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SecondStartCharacter",
                table: "configuracion_de_acceso",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EndCharacter",
                table: "configuracion_de_acceso",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CardLength",
                table: "configuracion_de_acceso",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Amount",
                table: "articulos",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "IsSuccessful",
                table: "accesos_socios",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "accesos_socios",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyMemberId",
                table: "accesos_socios",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_accesos_socios_accesos_AccesoId",
                table: "accesos_socios",
                column: "AccesoId",
                principalTable: "accesos",
                principalColumn: "id");
        }
    }
}
