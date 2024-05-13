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

        public DbSet<Deposito> Depositos { get; set; }
        public DbSet<Ferreteria> Ferreterias { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Motivos> Motivos { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<TipoDeMovimiento> TiposDeMovimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.Deposito_Destino)
                .WithMany()
                .HasForeignKey(m => m.Id)
                .OnDelete(DeleteBehavior.Restrict); // Esto asegura que no se elimine en cascada si eliminas un depósito

            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.Deposito_Destino)
                .WithMany()
                .HasForeignKey(m => m.Id)
                .OnDelete(DeleteBehavior.Restrict); // Esto asegura que no se elimine en cascada si eliminas un depósito

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
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

            base.OnModelCreating(modelBuilder);
        }
    }
}