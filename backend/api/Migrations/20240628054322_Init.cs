using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ferreterias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Str_nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Str_ruc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Str_telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bool_borrado = table.Column<bool>(type: "bit", nullable: false)
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
                    Bool_perdida = table.Column<bool>(type: "bit", nullable: false),
                    Bool_borrado = table.Column<bool>(type: "bit", nullable: false)
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
                    Str_correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bool_borrado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proveedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "timbrados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Str_timbrado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_inicio_vigencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date_fin_vigencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Codigo_establecimiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Punto_de_expedicion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Secuencia_actual = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timbrados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tipos_de_movimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Str_tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bool_operacion = table.Column<bool>(type: "bit", nullable: false),
                    Bool_borrado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_de_movimientos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "depositos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Str_nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Str_direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Str_telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bool_borrado = table.Column<bool>(type: "bit", nullable: false),
                    FerreteriaId = table.Column<int>(type: "int", nullable: true),
                    EncargadoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EncargadoUsername = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_depositos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_depositos_AspNetUsers_EncargadoId",
                        column: x => x.EncargadoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_depositos_ferreterias_FerreteriaId",
                        column: x => x.FerreteriaId,
                        principalTable: "ferreterias",
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
                    ProveedorId = table.Column<int>(type: "int", nullable: true),
                    Bool_borrado = table.Column<bool>(type: "bit", nullable: false)
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
                name: "motivos_por_tipo_de_movimiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Str_descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bool_borrado = table.Column<bool>(type: "bit", nullable: false),
                    MotivoId = table.Column<int>(type: "int", nullable: true),
                    TipodemovimientoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_motivos_por_tipo_de_movimiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_motivos_por_tipo_de_movimiento_motivos_MotivoId",
                        column: x => x.MotivoId,
                        principalTable: "motivos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_motivos_por_tipo_de_movimiento_tipos_de_movimientos_TipodemovimientoId",
                        column: x => x.TipodemovimientoId,
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
                    Dec_costo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Dec_costo_PPP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Int_iva = table.Column<int>(type: "int", nullable: false),
                    Dec_precio_mayorista = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Dec_precio_minorista = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bool_borrado = table.Column<bool>(type: "bit", nullable: false)
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
                name: "movimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date_fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bool_borrado = table.Column<bool>(type: "bit", nullable: false),
                    MotivoPorTipodeMovimientoId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_movimientos_motivos_por_tipo_de_movimiento_MotivoPorTipodeMovimientoId",
                        column: x => x.MotivoPorTipodeMovimientoId,
                        principalTable: "motivos_por_tipo_de_movimiento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "asientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    movimientoId = table.Column<int>(type: "int", nullable: true),
                    Str_cuenta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Str_concepto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Str_descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dec_debe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Dec_haber = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bool_borrado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_asientos_movimientos_movimientoId",
                        column: x => x.movimientoId,
                        principalTable: "movimientos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "detalles_de_movimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Int_cantidad = table.Column<int>(type: "int", nullable: false),
                    Dec_costo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MovimientoId = table.Column<int>(type: "int", nullable: true),
                    ProductoId = table.Column<int>(type: "int", nullable: true),
                    Bool_borrado = table.Column<bool>(type: "bit", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "notas_de_remision",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Str_numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimbradoId = table.Column<int>(type: "int", nullable: false),
                    Date_fecha_de_expedicion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date_fecha_de_vencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MovimientoId = table.Column<int>(type: "int", nullable: true),
                    EmpresaNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaDireccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaTelefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaSucursal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaActividad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ruc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinatarioNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinatarioDocumento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PuntoPartida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PuntoLlegada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrasladoFechaInicio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrasladoFechaFin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotivoDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComprobanteVenta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notas_de_remision", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notas_de_remision_movimientos_MovimientoId",
                        column: x => x.MovimientoId,
                        principalTable: "movimientos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_notas_de_remision_timbrados_TimbradoId",
                        column: x => x.TimbradoId,
                        principalTable: "timbrados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9b032b29-e73d-4f28-95cc-6e7bd9dbd061", null, "User", "USER" },
                    { "c626130e-a009-482a-bd50-0e6a1f46220a", null, "Admin", "ADMIN" },
                    { "f7859063-c322-4d28-9e49-5363394f7936", null, "Encargado", "ENCARGADO" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_asientos_movimientoId",
                table: "asientos",
                column: "movimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_categorias_ProveedorId",
                table: "categorias",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_depositos_EncargadoId",
                table: "depositos",
                column: "EncargadoId");

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
                name: "IX_motivos_por_tipo_de_movimiento_MotivoId",
                table: "motivos_por_tipo_de_movimiento",
                column: "MotivoId");

            migrationBuilder.CreateIndex(
                name: "IX_motivos_por_tipo_de_movimiento_TipodemovimientoId",
                table: "motivos_por_tipo_de_movimiento",
                column: "TipodemovimientoId");

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
                name: "IX_movimientos_MotivoPorTipodeMovimientoId",
                table: "movimientos",
                column: "MotivoPorTipodeMovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_notas_de_remision_MovimientoId",
                table: "notas_de_remision",
                column: "MovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_notas_de_remision_TimbradoId",
                table: "notas_de_remision",
                column: "TimbradoId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asientos");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropTable(
                name: "detalles_de_movimientos");

            migrationBuilder.DropTable(
                name: "notas_de_remision");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "movimientos");

            migrationBuilder.DropTable(
                name: "timbrados");

            migrationBuilder.DropTable(
                name: "marcas");

            migrationBuilder.DropTable(
                name: "depositos");

            migrationBuilder.DropTable(
                name: "motivos_por_tipo_de_movimiento");

            migrationBuilder.DropTable(
                name: "proveedores");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ferreterias");

            migrationBuilder.DropTable(
                name: "motivos");

            migrationBuilder.DropTable(
                name: "tipos_de_movimientos");
        }
    }
}
