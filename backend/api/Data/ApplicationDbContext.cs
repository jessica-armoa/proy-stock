using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuarios>{
        public ApplicationDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }

        public DbSet<Deposito> depositos { get; set; }
        public DbSet<Ferreteria> ferreterias { get; set; }
        public DbSet<Marca> marcas { get; set; }
        public DbSet<Motivos> motivos { get; set; }
        public DbSet<Movimiento> movimientos { get; set; }
        public DbSet<Producto> productos { get; set; }
        public DbSet<Proveedor> proveedores { get; set; }
        public DbSet<TipoDeMovimiento> tipos_de_movimientos { get; set; }
        public DbSet<Categoria> categorias { get; set; }
        public DbSet<DetalleDeMovimiento> detalles_de_movimientos { get; set; }
        public DbSet<NotaDeRemision> notas_de_remision { get; set; }
        public DbSet<Timbrado> timbrados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Proveedor>()
                .HasMany(p => p.Categorias)
                .WithOne(c => c.Proveedor)
                .HasForeignKey(c => c.ProveedorId);

            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.DepositoOrigen)
                .WithMany()
                .HasForeignKey(m => m.DepositoDestinoId)
                .OnDelete(DeleteBehavior.Restrict); // Esto asegura que no se elimine en cascada si eliminas un depósito

            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.DepositoDestino)
                .WithMany()
                .HasForeignKey(m => m.DepositoOrigenId)
                .OnDelete(DeleteBehavior.Restrict); // Esto asegura que no se elimine en cascada si eliminas un depósito

            modelBuilder.Entity<Deposito>()
            .HasOne(d => d.Encargado)
            .WithMany()
            .HasForeignKey(d => d.EncargadoId)
            .OnDelete(DeleteBehavior.Restrict);

            // Otros ajustes de modelo aquí si es necesario
            List<IdentityRole> roles = new List<IdentityRole>{
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Name = "Encargado",
                    NormalizedName = "ENCARGADO"
                },
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

            base.OnModelCreating(modelBuilder);
        }
    }
}