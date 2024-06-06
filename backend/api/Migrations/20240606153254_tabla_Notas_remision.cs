using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class tabla_Notas_remision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6801e97d-094d-44f9-85e0-3cfe8ccb0363");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bfec5579-4339-46e7-9116-ec0f909414c4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f82665cd-2f5e-4bb9-891d-07f10b33ac7d");

            migrationBuilder.DropColumn(
                name: "Bool_operacion",
                table: "tipos_de_movimientos");

            migrationBuilder.AddColumn<string>(
                name: "Str_descripcion",
                table: "tipos_de_movimientos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NotaDeRemisionId",
                table: "detalles_de_movimientos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "notas_de_remision",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Str_numero = table.Column<int>(type: "int", nullable: false),
                    Int_timbrado = table.Column<int>(type: "int", nullable: false),
                    Str_numero_de_comprobante_inicial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Str_numero_de_comprobante_final = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Str_numero_de_comprobante_actual = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_fecha_de_expedicion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date_fecha_de_vencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MovimientoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notas_de_remision", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notas_de_remision_movimientos_MovimientoId",
                        column: x => x.MovimientoId,
                        principalTable: "movimientos",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "304da5ea-a664-4b5a-8bae-6fb499f8c276", null, "Encargado", "ENCARGADO" },
                    { "5104e589-59a1-41e2-95bf-3339a68427c4", null, "User", "USER" },
                    { "f4a7c2af-0ae5-4dee-bfa8-f92e19eda8aa", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_detalles_de_movimientos_NotaDeRemisionId",
                table: "detalles_de_movimientos",
                column: "NotaDeRemisionId");

            migrationBuilder.CreateIndex(
                name: "IX_notas_de_remision_MovimientoId",
                table: "notas_de_remision",
                column: "MovimientoId");

            migrationBuilder.AddForeignKey(
                name: "FK_detalles_de_movimientos_notas_de_remision_NotaDeRemisionId",
                table: "detalles_de_movimientos",
                column: "NotaDeRemisionId",
                principalTable: "notas_de_remision",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_detalles_de_movimientos_notas_de_remision_NotaDeRemisionId",
                table: "detalles_de_movimientos");

            migrationBuilder.DropTable(
                name: "notas_de_remision");

            migrationBuilder.DropIndex(
                name: "IX_detalles_de_movimientos_NotaDeRemisionId",
                table: "detalles_de_movimientos");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "304da5ea-a664-4b5a-8bae-6fb499f8c276");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5104e589-59a1-41e2-95bf-3339a68427c4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4a7c2af-0ae5-4dee-bfa8-f92e19eda8aa");

            migrationBuilder.DropColumn(
                name: "Str_descripcion",
                table: "tipos_de_movimientos");

            migrationBuilder.DropColumn(
                name: "NotaDeRemisionId",
                table: "detalles_de_movimientos");

            migrationBuilder.AddColumn<bool>(
                name: "Bool_operacion",
                table: "tipos_de_movimientos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6801e97d-094d-44f9-85e0-3cfe8ccb0363", null, "Encargado", "ENCARGADO" },
                    { "bfec5579-4339-46e7-9116-ec0f909414c4", null, "User", "USER" },
                    { "f82665cd-2f5e-4bb9-891d-07f10b33ac7d", null, "Admin", "ADMIN" }
                });
        }
    }
}
