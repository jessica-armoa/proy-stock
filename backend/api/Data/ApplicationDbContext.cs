using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext : DbContext
    {
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.Deposito_origen)
                .WithMany()
                .HasForeignKey(m => m.Fk_deposito_destino)
                .OnDelete(DeleteBehavior.Restrict); // Esto asegura que no se elimine en cascada si eliminas un depósito

            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.Deposito_destino)
                .WithMany()
                .HasForeignKey(m => m.Fk_deposito_origen)
                .OnDelete(DeleteBehavior.Restrict); // Esto asegura que no se elimine en cascada si eliminas un depósito

            // Otros ajustes de modelo aquí si es necesario

            base.OnModelCreating(modelBuilder);
        }
    }
}