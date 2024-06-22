using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class EncargadoDeposito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32ad10d4-d5de-4792-8335-72ef64d9775f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd3eed36-3702-4c01-9043-7d679bcf85c9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e59c3450-5d85-4dff-bfae-7e0379c921a7");

            migrationBuilder.DropColumn(
                name: "Str_encargado",
                table: "depositos");

            migrationBuilder.DropColumn(
                name: "Str_telefonoEncargado",
                table: "depositos");

            migrationBuilder.AddColumn<string>(
                name: "EncargadoId",
                table: "depositos",
                type: "nvarchar(450)",
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

            migrationBuilder.CreateIndex(
                name: "IX_depositos_EncargadoId",
                table: "depositos",
                column: "EncargadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_depositos_AspNetUsers_EncargadoId",
                table: "depositos",
                column: "EncargadoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_depositos_AspNetUsers_EncargadoId",
                table: "depositos");

            migrationBuilder.DropIndex(
                name: "IX_depositos_EncargadoId",
                table: "depositos");

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
                name: "EncargadoId",
                table: "depositos");

            migrationBuilder.AddColumn<string>(
                name: "Str_encargado",
                table: "depositos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Str_telefonoEncargado",
                table: "depositos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "32ad10d4-d5de-4792-8335-72ef64d9775f", null, "User", "USER" },
                    { "dd3eed36-3702-4c01-9043-7d679bcf85c9", null, "Encargado", "ENCARGADO" },
                    { "e59c3450-5d85-4dff-bfae-7e0379c921a7", null, "Admin", "ADMIN" }
                });
        }
    }
}
