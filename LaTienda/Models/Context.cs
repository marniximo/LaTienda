using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LaTienda.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> opt) : base(opt)
        {

        }

        public void ApplySeeds() {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LineaStock>()
                .HasOne(l => l.Producto)
                .WithMany(p => p.LineasStock)
                .HasForeignKey(l => l.IdProducto);

            modelBuilder.Entity<LineaStock>()
                .HasOne(l => l.Color)
                .WithMany()
                .HasForeignKey(l => l.IdColor);

            modelBuilder.Entity<LineaStock>()
                .HasOne(l => l.Talle)
                .WithMany()
                .HasForeignKey(l => l.IdTalle);

            modelBuilder.Entity<LineaStock>()
                .HasOne(l => l.Sucursal)
                .WithMany()
                .HasForeignKey(l => l.CodigoSucursal);

            modelBuilder.Entity<LineaVenta>()
                .HasOne(l => l.Producto)
                .WithMany(p => p.LineasVenta)
                .HasForeignKey(l => l.IdProducto);

            modelBuilder.Entity<LineaVenta>()
                .HasOne(l => l.Venta)
                .WithMany(v => v.LineasVenta)
                .HasForeignKey(l => l.IdVenta);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Marca)
                .WithMany(m => m.Productos)
                .HasForeignKey(p => p.IdMarca);

            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Cliente)
                .WithMany(m => m.Ventas)
                .HasForeignKey(p => p.CUITCliente);

            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Vendedor)
                .WithMany(v => v.Ventas)
                .HasForeignKey(v => v.IdVendedor);

            modelBuilder.Entity<TicketAutenticacion>();

            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.Sucursal)
                .WithMany(s => s.Empleados)
                .HasForeignKey(e => e.CodigoSucursal);

            modelBuilder.Entity<Marca>()
                .HasMany(m=>m.Productos)
                .WithOne(p=>p.Marca)
                .HasForeignKey(p=>p.IdMarca);

        }

        public DbSet<TicketAutenticacion> Tickets { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Color> Colores { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<LineaStock> LineasStock { get; set; }
        public DbSet<LineaVenta> LineasVenta { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Talle> Talles { get; set; }
        public DbSet<Venta> Ventas { get; set; }
    }
}
