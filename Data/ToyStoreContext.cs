using Jugueton.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Jugueton.Data
{
    public class ToyStoreContext : DbContext
    {
        public ToyStoreContext(DbContextOptions<ToyStoreContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Data
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "Alemania Correa" },
                new User { UserId = 2, Username = "Katty Salvador" },
                new User { UserId = 3, Username = "Stalin Aviles" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Action Figure", Price = 10.99m },
                new Product { ProductId = 2, Name = "Doll", Price = 12.99m },
                new Product { ProductId = 3, Name = "Toy Castle", Price = 17.99m }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, OrderDate = DateTime.Now, UserId = 1 },
                new Order { OrderId = 2, OrderDate = DateTime.Now, UserId = 2 }
            );

            modelBuilder.Entity<OrderProduct>().HasData(
                new OrderProduct { OrderId = 1, ProductId = 1, Quantity = 2 },
                new OrderProduct { OrderId = 1, ProductId = 2, Quantity = 3 },
                new OrderProduct { OrderId = 2, ProductId = 3, Quantity = 1 }
            );

            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductId);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}

