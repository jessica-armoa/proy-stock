using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class tabla_Notas_remision_modificada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4406daa5-d78e-4b63-87ae-05f6ced3d839");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7eaf94f4-1e75-42c0-a6e1-664596d02c45");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa0b1b6e-b535-4fc2-98c5-611f2f919550");

            migrationBuilder.AddColumn<string>(
                name: "ComprobanteVenta",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ConductorDireccion",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ConductorDocumento",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ConductorNombre",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DestinatarioDocumento",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DestinatarioNombre",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmpresaActividad",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmpresaDireccion",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmpresaNombre",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmpresaSucursal",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmpresaTelefono",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Motivo",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MotivoDescripcion",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PuntoLlegada",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PuntoPartida",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ruc",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TransportistaNombre",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TransportistaRuc",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TrasladoFechaFin",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TrasladoFechaInicio",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TrasladoRua",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TrasladoVehiculo",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3e5a7970-f206-47eb-acf8-a0e1f84782c7", null, "User", "USER" },
                    { "940b3c0b-7e60-474c-95fc-ae493a31ff95", null, "Admin", "ADMIN" },
                    { "e40df266-077e-4ada-9679-8a9faed5802d", null, "Encargado", "ENCARGADO" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e5a7970-f206-47eb-acf8-a0e1f84782c7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "940b3c0b-7e60-474c-95fc-ae493a31ff95");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e40df266-077e-4ada-9679-8a9faed5802d");

            migrationBuilder.DropColumn(
                name: "ComprobanteVenta",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "ConductorDireccion",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "ConductorDocumento",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "ConductorNombre",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "DestinatarioDocumento",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "DestinatarioNombre",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "EmpresaActividad",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "EmpresaDireccion",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "EmpresaNombre",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "EmpresaSucursal",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "EmpresaTelefono",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "Motivo",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "MotivoDescripcion",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "PuntoLlegada",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "PuntoPartida",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "Ruc",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "TransportistaNombre",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "TransportistaRuc",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "TrasladoFechaFin",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "TrasladoFechaInicio",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "TrasladoRua",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "TrasladoVehiculo",
                table: "notas_de_remision");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4406daa5-d78e-4b63-87ae-05f6ced3d839", null, "User", "USER" },
                    { "7eaf94f4-1e75-42c0-a6e1-664596d02c45", null, "Encargado", "ENCARGADO" },
                    { "aa0b1b6e-b535-4fc2-98c5-611f2f919550", null, "Admin", "ADMIN" }
                });
        }
    }
}
