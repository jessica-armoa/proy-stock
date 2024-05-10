using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ferreterias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Str_nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Str_ruc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Str_telefono = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ferreterias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "motivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Str_motivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bool_perdida = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_motivos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Str_nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Str_telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Str_direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Str_correo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proveedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "depositos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Str_nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Str_direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FerreteriaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_depositos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_depositos_ferreterias_FerreteriaId",
                        column: x => x.FerreteriaId,
                        principalTable: "ferreterias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tipos_de_movimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bool_operacion = table.Column<bool>(type: "bit", nullable: false),
                    MotivoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_de_movimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tipos_de_movimientos_motivos_MotivoId",
                        column: x => x.MotivoId,
                        principalTable: "motivos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Str_descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_categorias_proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "proveedores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "marcas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Str_nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marcas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_marcas_proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "proveedores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "movimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date_fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoDeMovimientoId = table.Column<int>(type: "int", nullable: true),
                    DepositoOrigenId = table.Column<int>(type: "int", nullable: true),
                    DepositoDestinoId = table.Column<int>(type: "int", nullable: true),
                    DepositoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_movimientos_depositos_DepositoDestinoId",
                        column: x => x.DepositoDestinoId,
                        principalTable: "depositos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_movimientos_depositos_DepositoId",
                        column: x => x.DepositoId,
                        principalTable: "depositos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_movimientos_depositos_DepositoOrigenId",
                        column: x => x.DepositoOrigenId,
                        principalTable: "depositos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_movimientos_tipos_de_movimientos_TipoDeMovimientoId",
                        column: x => x.TipoDeMovimientoId,
                        principalTable: "tipos_de_movimientos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Str_ruta_imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Str_nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Str_descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepositoId = table.Column<int>(type: "int", nullable: true),
                    ProveedorId = table.Column<int>(type: "int", nullable: true),
                    MarcaId = table.Column<int>(type: "int", nullable: true),
                    Int_cantidad_actual = table.Column<int>(type: "int", nullable: false),
                    Int_cantidad_minima = table.Column<int>(type: "int", nullable: false),
                    Dec_costo_PPP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Int_iva = table.Column<int>(type: "int", nullable: false),
                    Dec_precio_mayorista = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Dec_precio_minorista = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productos_depositos_DepositoId",
                        column: x => x.DepositoId,
                        principalTable: "depositos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_productos_marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "marcas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_productos_proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "proveedores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "detalles_de_movimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Int_cantidad = table.Column<int>(type: "int", nullable: false),
                    MovimientoId = table.Column<int>(type: "int", nullable: true),
                    ProductoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalles_de_movimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_detalles_de_movimientos_movimientos_MovimientoId",
                        column: x => x.MovimientoId,
                        principalTable: "movimientos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_detalles_de_movimientos_productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "productos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_categorias_ProveedorId",
                table: "categorias",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_depositos_FerreteriaId",
                table: "depositos",
                column: "FerreteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_detalles_de_movimientos_MovimientoId",
                table: "detalles_de_movimientos",
                column: "MovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_detalles_de_movimientos_ProductoId",
                table: "detalles_de_movimientos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_marcas_ProveedorId",
                table: "marcas",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_movimientos_DepositoDestinoId",
                table: "movimientos",
                column: "DepositoDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_movimientos_DepositoId",
                table: "movimientos",
                column: "DepositoId");

            migrationBuilder.CreateIndex(
                name: "IX_movimientos_DepositoOrigenId",
                table: "movimientos",
                column: "DepositoOrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_movimientos_TipoDeMovimientoId",
                table: "movimientos",
                column: "TipoDeMovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_productos_DepositoId",
                table: "productos",
                column: "DepositoId");

            migrationBuilder.CreateIndex(
                name: "IX_productos_MarcaId",
                table: "productos",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_productos_ProveedorId",
                table: "productos",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_tipos_de_movimientos_MotivoId",
                table: "tipos_de_movimientos",
                column: "MotivoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropTable(
                name: "detalles_de_movimientos");

            migrationBuilder.DropTable(
                name: "movimientos");

            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "tipos_de_movimientos");

            migrationBuilder.DropTable(
                name: "depositos");

            migrationBuilder.DropTable(
                name: "marcas");

            migrationBuilder.DropTable(
                name: "motivos");

            migrationBuilder.DropTable(
                name: "ferreterias");

            migrationBuilder.DropTable(
                name: "proveedores");
        }
    }
}
