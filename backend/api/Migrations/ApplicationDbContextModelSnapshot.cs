﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Data;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
<<<<<<< HEAD
                            Id = "a7b867b0-aa50-4c7b-8941-2a9e306d0108",
=======
                            Id = "f58b8fbd-7bb5-4ba9-8ad0-a549576b19a7",
>>>>>>> 7a9c24b0fc62c75315ce14e1bcc1c3163bd088e5
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
<<<<<<< HEAD
                            Id = "940d3c6a-4d09-4196-adfb-f742b2a6b7ab",
=======
                            Id = "1d647d85-bd9b-4fb0-8982-0c42f2a440be",
>>>>>>> 7a9c24b0fc62c75315ce14e1bcc1c3163bd088e5
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
<<<<<<< HEAD
                            Id = "d4dce078-3ab2-40e1-9c79-3a6238dd5abb",
=======
                            Id = "3e20e71e-b931-4fb1-ab72-e7223908c10c",
>>>>>>> 7a9c24b0fc62c75315ce14e1bcc1c3163bd088e5
                            Name = "Encargado",
                            NormalizedName = "ENCARGADO"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("api.Models.Asiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Bool_borrado")
                        .HasColumnType("bit");

                    b.Property<decimal>("Dec_debe")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Dec_haber")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Str_concepto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Str_cuenta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Str_descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("movimientoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("movimientoId");

                    b.ToTable("asientos");
                });

            modelBuilder.Entity("api.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ProveedorId")
                        .HasColumnType("int");

                    b.Property<string>("Str_descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProveedorId");

                    b.ToTable("categorias");
                });

            modelBuilder.Entity("api.Models.Deposito", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Bool_borrado")
                        .HasColumnType("bit");

                    b.Property<string>("EncargadoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EncargadoUsername")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FerreteriaId")
                        .HasColumnType("int");

                    b.Property<string>("Str_direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Str_nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Str_telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EncargadoId");

                    b.HasIndex("FerreteriaId");

                    b.ToTable("depositos");
                });

            modelBuilder.Entity("api.Models.DetalleDeMovimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Bool_borrado")
                        .HasColumnType("bit");

                    b.Property<decimal>("Dec_costo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Int_cantidad")
                        .HasColumnType("int");

                    b.Property<int?>("MovimientoId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovimientoId");

                    b.HasIndex("ProductoId");

                    b.ToTable("detalles_de_movimientos");
                });

            modelBuilder.Entity("api.Models.Ferreteria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Bool_borrado")
                        .HasColumnType("bit");

                    b.Property<string>("Str_nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Str_ruc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Str_telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ferreterias");
                });

            modelBuilder.Entity("api.Models.Marca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Bool_borrado")
                        .HasColumnType("bit");

                    b.Property<int?>("ProveedorId")
                        .HasColumnType("int");

                    b.Property<string>("Str_nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProveedorId");

                    b.ToTable("marcas");
                });

            modelBuilder.Entity("api.Models.Motivo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Bool_borrado")
                        .HasColumnType("bit");

                    b.Property<bool>("Bool_perdida")
                        .HasColumnType("bit");

                    b.Property<string>("Str_motivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("motivos");
                });

            modelBuilder.Entity("api.Models.MotivoPorTipoDeMovimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Bool_borrado")
                        .HasColumnType("bit");

                    b.Property<int?>("MotivoId")
                        .HasColumnType("int");

                    b.Property<string>("Str_descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TipodemovimientoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MotivoId");

                    b.HasIndex("TipodemovimientoId");

                    b.ToTable("motivos_por_tipo_de_movimiento");
                });

            modelBuilder.Entity("api.Models.Movimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Bool_borrado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Date_fecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepositoDestinoId")
                        .HasColumnType("int");

                    b.Property<int?>("DepositoId")
                        .HasColumnType("int");

                    b.Property<int?>("DepositoOrigenId")
                        .HasColumnType("int");

                    b.Property<int?>("MotivoPorTipodeMovimientoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepositoDestinoId");

                    b.HasIndex("DepositoId");

                    b.HasIndex("DepositoOrigenId");

                    b.HasIndex("MotivoPorTipodeMovimientoId");

                    b.ToTable("movimientos");
                });

            modelBuilder.Entity("api.Models.NotaDeRemision", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ComprobanteVenta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date_fecha_de_expedicion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date_fecha_de_vencimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("DestinatarioDocumento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DestinatarioNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmpresaActividad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmpresaDireccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmpresaNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmpresaSucursal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmpresaTelefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Motivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotivoDescripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MovimientoId")
                        .HasColumnType("int");

                    b.Property<string>("PuntoLlegada")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PuntoPartida")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ruc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Str_numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TimbradoId")
                        .HasColumnType("int");

                    b.Property<string>("TrasladoFechaFin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrasladoFechaInicio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MovimientoId");

                    b.HasIndex("TimbradoId");

                    b.ToTable("notas_de_remision");
                });

            modelBuilder.Entity("api.Models.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Bool_borrado")
                        .HasColumnType("bit");

                    b.Property<decimal>("Dec_costo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Dec_costo_PPP")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Dec_precio_mayorista")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Dec_precio_minorista")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("DepositoId")
                        .HasColumnType("int");

                    b.Property<int>("Int_cantidad_actual")
                        .HasColumnType("int");

                    b.Property<int>("Int_cantidad_minima")
                        .HasColumnType("int");

                    b.Property<int>("Int_iva")
                        .HasColumnType("int");

                    b.Property<int?>("MarcaId")
                        .HasColumnType("int");

                    b.Property<int?>("ProveedorId")
                        .HasColumnType("int");

                    b.Property<string>("Str_descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Str_nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Str_ruta_imagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepositoId");

                    b.HasIndex("MarcaId");

                    b.HasIndex("ProveedorId");

                    b.ToTable("productos");
                });

            modelBuilder.Entity("api.Models.Proveedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Bool_borrado")
                        .HasColumnType("bit");

                    b.Property<string>("Str_correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Str_direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Str_nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Str_telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("proveedores");
                });

            modelBuilder.Entity("api.Models.Timbrado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Codigo_establecimiento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date_fin_vigencia")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date_inicio_vigencia")
                        .HasColumnType("datetime2");

                    b.Property<string>("Punto_de_expedicion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Secuencia_actual")
                        .HasColumnType("int");

                    b.Property<string>("Str_timbrado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("timbrados");
                });

            modelBuilder.Entity("api.Models.TipoDeMovimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Bool_borrado")
                        .HasColumnType("bit");

                    b.Property<bool>("Bool_operacion")
                        .HasColumnType("bit");

                    b.Property<string>("Str_tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tipos_de_movimientos");
                });

            modelBuilder.Entity("api.Models.Usuarios", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("api.Models.Usuarios", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("api.Models.Usuarios", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.Usuarios", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("api.Models.Usuarios", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.Models.Asiento", b =>
                {
                    b.HasOne("api.Models.Movimiento", "Movimiento")
                        .WithMany()
                        .HasForeignKey("movimientoId");

                    b.Navigation("Movimiento");
                });

            modelBuilder.Entity("api.Models.Categoria", b =>
                {
                    b.HasOne("api.Models.Proveedor", "Proveedor")
                        .WithMany("Categorias")
                        .HasForeignKey("ProveedorId");

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("api.Models.Deposito", b =>
                {
                    b.HasOne("api.Models.Usuarios", "Encargado")
                        .WithMany()
                        .HasForeignKey("EncargadoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("api.Models.Ferreteria", "Ferreteria")
                        .WithMany("Depositos")
                        .HasForeignKey("FerreteriaId");

                    b.Navigation("Encargado");

                    b.Navigation("Ferreteria");
                });

            modelBuilder.Entity("api.Models.DetalleDeMovimiento", b =>
                {
                    b.HasOne("api.Models.Movimiento", "Movimiento")
                        .WithMany("DetallesDeMovimientos")
                        .HasForeignKey("MovimientoId");

                    b.HasOne("api.Models.Producto", "Producto")
                        .WithMany("DetallesDeMovimientos")
                        .HasForeignKey("ProductoId");

                    b.Navigation("Movimiento");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("api.Models.Marca", b =>
                {
                    b.HasOne("api.Models.Proveedor", "Proveedor")
                        .WithMany("Marcas")
                        .HasForeignKey("ProveedorId");

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("api.Models.MotivoPorTipoDeMovimiento", b =>
                {
                    b.HasOne("api.Models.Motivo", "Motivo")
                        .WithMany("MotivosPorTipoDeMovimiento")
                        .HasForeignKey("MotivoId");

                    b.HasOne("api.Models.TipoDeMovimiento", "TipoDeMovimiento")
                        .WithMany("MotivosPorTipoDeMovimiento")
                        .HasForeignKey("TipodemovimientoId");

                    b.Navigation("Motivo");

                    b.Navigation("TipoDeMovimiento");
                });

            modelBuilder.Entity("api.Models.Movimiento", b =>
                {
                    b.HasOne("api.Models.Deposito", "DepositoOrigen")
                        .WithMany()
                        .HasForeignKey("DepositoDestinoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("api.Models.Deposito", null)
                        .WithMany("Movimientos")
                        .HasForeignKey("DepositoId");

                    b.HasOne("api.Models.Deposito", "DepositoDestino")
                        .WithMany()
                        .HasForeignKey("DepositoOrigenId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("api.Models.MotivoPorTipoDeMovimiento", "MotivoPorTipoDeMovimiento")
                        .WithMany("Movimientos")
                        .HasForeignKey("MotivoPorTipodeMovimientoId");

                    b.Navigation("DepositoDestino");

                    b.Navigation("DepositoOrigen");

                    b.Navigation("MotivoPorTipoDeMovimiento");
                });

            modelBuilder.Entity("api.Models.NotaDeRemision", b =>
                {
                    b.HasOne("api.Models.Movimiento", "Movimiento")
                        .WithMany()
                        .HasForeignKey("MovimientoId");

                    b.HasOne("api.Models.Timbrado", "Timbrado")
                        .WithMany()
                        .HasForeignKey("TimbradoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movimiento");

                    b.Navigation("Timbrado");
                });

            modelBuilder.Entity("api.Models.Producto", b =>
                {
                    b.HasOne("api.Models.Deposito", "Deposito")
                        .WithMany("Productos")
                        .HasForeignKey("DepositoId");

                    b.HasOne("api.Models.Marca", "Marca")
                        .WithMany("Productos")
                        .HasForeignKey("MarcaId");

                    b.HasOne("api.Models.Proveedor", "Proveedor")
                        .WithMany("Productos")
                        .HasForeignKey("ProveedorId");

                    b.Navigation("Deposito");

                    b.Navigation("Marca");

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("api.Models.Deposito", b =>
                {
                    b.Navigation("Movimientos");

                    b.Navigation("Productos");
                });

            modelBuilder.Entity("api.Models.Ferreteria", b =>
                {
                    b.Navigation("Depositos");
                });

            modelBuilder.Entity("api.Models.Marca", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("api.Models.Motivo", b =>
                {
                    b.Navigation("MotivosPorTipoDeMovimiento");
                });

            modelBuilder.Entity("api.Models.MotivoPorTipoDeMovimiento", b =>
                {
                    b.Navigation("Movimientos");
                });

            modelBuilder.Entity("api.Models.Movimiento", b =>
                {
                    b.Navigation("DetallesDeMovimientos");
                });

            modelBuilder.Entity("api.Models.Producto", b =>
                {
                    b.Navigation("DetallesDeMovimientos");
                });

            modelBuilder.Entity("api.Models.Proveedor", b =>
                {
                    b.Navigation("Categorias");

                    b.Navigation("Marcas");

                    b.Navigation("Productos");
                });

            modelBuilder.Entity("api.Models.TipoDeMovimiento", b =>
                {
                    b.Navigation("MotivosPorTipoDeMovimiento");
                });
#pragma warning restore 612, 618
        }
    }
}
