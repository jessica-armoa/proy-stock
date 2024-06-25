using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class NotaRemisionYTimbrado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "053ce821-6f00-4b56-b212-c5d3c1d70ac1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14909982-f042-4f92-88f8-fde3554aca2c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2949bda-19e7-402d-9cf2-8ac0d1872dc9");

            migrationBuilder.DropColumn(
                name: "Str_numero_de_comprobante_actual",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "Str_numero_de_comprobante_final",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "Str_numero_de_comprobante_inicial",
                table: "notas_de_remision");

            migrationBuilder.DropColumn(
                name: "Str_timbrado",
                table: "notas_de_remision");

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "timbrados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Codigo_establecimiento",
                table: "timbrados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Punto_de_expedicion",
                table: "timbrados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Secuencia_actual",
                table: "timbrados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimbradoId",
                table: "notas_de_remision",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1942d817-c287-4dea-ba00-0951d1314efa", null, "User", "USER" },
                    { "1a1f049f-769e-4873-bde7-03723f6d7830", null, "Encargado", "ENCARGADO" },
                    { "d3449252-639c-4ad9-b568-d1d0cd220deb", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_notas_de_remision_TimbradoId",
                table: "notas_de_remision",
                column: "TimbradoId");

            migrationBuilder.AddForeignKey(
                name: "FK_notas_de_remision_timbrados_TimbradoId",
                table: "notas_de_remision",
                column: "TimbradoId",
                principalTable: "timbrados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notas_de_remision_timbrados_TimbradoId",
                table: "notas_de_remision");

            migrationBuilder.DropIndex(
                name: "IX_notas_de_remision_TimbradoId",
                table: "notas_de_remision");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1942d817-c287-4dea-ba00-0951d1314efa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a1f049f-769e-4873-bde7-03723f6d7830");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3449252-639c-4ad9-b568-d1d0cd220deb");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "timbrados");

            migrationBuilder.DropColumn(
                name: "Codigo_establecimiento",
                table: "timbrados");

            migrationBuilder.DropColumn(
                name: "Punto_de_expedicion",
                table: "timbrados");

            migrationBuilder.DropColumn(
                name: "Secuencia_actual",
                table: "timbrados");

            migrationBuilder.DropColumn(
                name: "TimbradoId",
                table: "notas_de_remision");

            migrationBuilder.AddColumn<string>(
                name: "Str_numero_de_comprobante_actual",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Str_numero_de_comprobante_final",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Str_numero_de_comprobante_inicial",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Str_timbrado",
                table: "notas_de_remision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "053ce821-6f00-4b56-b212-c5d3c1d70ac1", null, "Admin", "ADMIN" },
                    { "14909982-f042-4f92-88f8-fde3554aca2c", null, "User", "USER" },
                    { "a2949bda-19e7-402d-9cf2-8ac0d1872dc9", null, "Encargado", "ENCARGADO" }
                });
        }
    }
}
