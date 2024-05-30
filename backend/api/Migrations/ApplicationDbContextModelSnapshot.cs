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
                            Id = "f82665cd-2f5e-4bb9-891d-07f10b33ac7d",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "bfec5579-4339-46e7-9116-ec0f909414c4",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "6801e97d-094d-44f9-85e0-3cfe8ccb0363",
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

                    b.Property<int?>("FerreteriaId")
                        .HasColumnType("int");

                    b.Property<string>("Str_direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Str_encargado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Str_nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Str_telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Str_telefonoEncargado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FerreteriaId");

                    b.ToTable("depositos");
                });

            modelBuilder.Entity("api.Models.DetalleDeMovimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

                    b.Property<int?>("ProveedorId")
                        .HasColumnType("int");

                    b.Property<string>("Str_nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProveedorId");

                    b.ToTable("marcas");
                });

            modelBuilder.Entity("api.Models.Motivos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Bool_perdida")
                        .HasColumnType("bit");

                    b.Property<string>("Str_motivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("motivos");
                });

            modelBuilder.Entity("api.Models.Movimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date_fecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepositoDestinoId")
                        .HasColumnType("int");

                    b.Property<int?>("DepositoId")
                        .HasColumnType("int");

                    b.Property<int?>("DepositoOrigenId")
                        .HasColumnType("int");

                    b.Property<int?>("TipoDeMovimientoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepositoDestinoId");

                    b.HasIndex("DepositoId");

                    b.HasIndex("DepositoOrigenId");

                    b.HasIndex("TipoDeMovimientoId");

                    b.ToTable("movimientos");
                });

            modelBuilder.Entity("api.Models.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

            modelBuilder.Entity("api.Models.TipoDeMovimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Bool_operacion")
                        .HasColumnType("bit");

                    b.Property<int?>("MotivoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MotivoId");

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

            modelBuilder.Entity("api.Models.Categoria", b =>
                {
                    b.HasOne("api.Models.Proveedor", "Proveedor")
                        .WithMany("Categorias")
                        .HasForeignKey("ProveedorId");

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("api.Models.Deposito", b =>
                {
                    b.HasOne("api.Models.Ferreteria", "Ferreteria")
                        .WithMany("Depositos")
                        .HasForeignKey("FerreteriaId");

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

                    b.HasOne("api.Models.TipoDeMovimiento", "TipoDeMovimiento")
                        .WithMany("Movimientos")
                        .HasForeignKey("TipoDeMovimientoId");

                    b.Navigation("DepositoDestino");

                    b.Navigation("DepositoOrigen");

                    b.Navigation("TipoDeMovimiento");
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

            modelBuilder.Entity("api.Models.TipoDeMovimiento", b =>
                {
                    b.HasOne("api.Models.Motivos", "Motivo")
                        .WithMany("Tipo_de_movimientos")
                        .HasForeignKey("MotivoId");

                    b.Navigation("Motivo");
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

            modelBuilder.Entity("api.Models.Motivos", b =>
                {
                    b.Navigation("Tipo_de_movimientos");
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
                    b.Navigation("Movimientos");
                });
#pragma warning restore 612, 618
        }
    }
}
