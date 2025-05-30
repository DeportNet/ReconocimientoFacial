using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeportNetReconocimiento.Migrations
{
    /// <inheritdoc />
    public partial class configInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accesos",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ActiveBranchId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProcessId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accesos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "articulos",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_dx = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<string>(type: "TEXT", nullable: false),
                    IsSaleItem = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articulos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "configuracion_de_acceso",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CardLength = table.Column<int>(type: "INTEGER", nullable: false),
                    StartCharacter = table.Column<string>(type: "TEXT", nullable: false),
                    EndCharacter = table.Column<string>(type: "TEXT", nullable: false),
                    SecondStartCharacter = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_configuracion_de_acceso", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "configuracion_general",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CantMaxLotes = table.Column<int>(type: "INTEGER", nullable: false),
                    ContraseniaBd = table.Column<string>(type: "TEXT", nullable: false),
                    NombreSucursal = table.Column<string>(type: "TEXT", nullable: false),
                    UltimaFechaSincronizacion = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AnteriorFechaSincronizacion = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_configuracion_general", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "empleados",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyMemberId = table.Column<int>(type: "INTEGER", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    isActive = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empleados", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "membresias",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_dx = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<string>(type: "TEXT", nullable: false),
                    IsSaleItem = table.Column<string>(type: "TEXT", nullable: false),
                    Period = table.Column<string>(type: "TEXT", nullable: false),
                    Days = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_membresias", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "socios",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_dx = table.Column<int>(type: "INTEGER", nullable: true),
                    email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    id_number = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    birth_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    cellphone = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    is_active = table.Column<string>(type: "TEXT", maxLength: 1, nullable: true),
                    card_number = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    address = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    address_floor = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    image_url = table.Column<string>(type: "TEXT", nullable: true),
                    gender = table.Column<string>(type: "TEXT", maxLength: 1, nullable: true),
                    is_valid = table.Column<string>(type: "TEXT", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_socios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "accesos_socios",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    MemberId = table.Column<string>(type: "TEXT", nullable: false),
                    AccessDate = table.Column<string>(type: "TEXT", nullable: false),
                    IsSuccessful = table.Column<string>(type: "TEXT", nullable: false),
                    AccesoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accesos_socios", x => x.id);
                    table.ForeignKey(
                        name: "FK_accesos_socios_accesos_AccesoId",
                        column: x => x.AccesoId,
                        principalTable: "accesos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_accesos_socios_AccesoId",
                table: "accesos_socios",
                column: "AccesoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accesos_socios");

            migrationBuilder.DropTable(
                name: "articulos");

            migrationBuilder.DropTable(
                name: "configuracion_de_acceso");

            migrationBuilder.DropTable(
                name: "configuracion_general");

            migrationBuilder.DropTable(
                name: "empleados");

            migrationBuilder.DropTable(
                name: "membresias");

            migrationBuilder.DropTable(
                name: "socios");

            migrationBuilder.DropTable(
                name: "accesos");
        }
    }
}
