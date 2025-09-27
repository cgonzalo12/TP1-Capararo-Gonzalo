using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class ApplicationDbContext : DbContext
    {
        //DBSETs
        public DbSet<Dish> Dish { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<DeliveryType> DeliveryType { get; set; }
        //Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //additional model settings

            //---Entity Dish
            modelBuilder.Entity<Dish>().HasKey(d => d.DishId);
            modelBuilder.Entity<Dish>().Property(d => d.DishId).IsRequired();
            modelBuilder.Entity<Dish>().Property(d => d.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Dish>().Property(d => d.Price).IsRequired().HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Dish>().Property(d => d.Available).IsRequired().HasDefaultValue(true);
            modelBuilder.Entity<Dish>().Property(d => d.CreateDate).IsRequired();
            modelBuilder.Entity<Dish>().Property(d => d.UpdateDate).IsRequired();
            modelBuilder.Entity<Dish>().Property(d => d.Description).HasMaxLength(1000);
            modelBuilder.Entity<Dish>().Property(d => d.ImageUrl).HasMaxLength(2086);//max URL length
            //Foreign key relationship with Category
            modelBuilder.Entity<Dish>()
                .HasOne<Category>(d => d.CategoryNav) //has a category
                .WithMany(c => c.Dishes) //a category has many dishes
                .HasForeignKey(d => d.Category);

            //--Entity Category
            modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<Category>().Property(c => c.Id).IsRequired();
            modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(25);
            modelBuilder.Entity<Category>().Property(c => c.Description).HasMaxLength(255);

            //--Entity OrderItem
            modelBuilder.Entity<OrderItem>().HasKey(oi => oi.OrderItemId);
            modelBuilder.Entity<OrderItem>().Property(oi => oi.OrderItemId).IsRequired();
            modelBuilder.Entity<OrderItem>().Property(oi => oi.Quantity).IsRequired().HasDefaultValue(1);
            modelBuilder.Entity<OrderItem>().Property(oi => oi.Notes).HasMaxLength(500);
            modelBuilder.Entity<OrderItem>().Property(oi => oi.CreateDate).IsRequired();
            //Foreign key relationship with Dish
            modelBuilder.Entity<OrderItem>()
                .HasOne<Dish>(oi => oi.DishNav) //has a Dish
                .WithMany(d => d.OrdersItems) //a Dish has many OrderItems
                .HasForeignKey(oi => oi.Dish);
            //Foreign key relationship with Order
            modelBuilder.Entity<OrderItem>()
                .HasOne<Order>(oi => oi.OrderNav) //has an Order
                .WithMany(o => o.OrderItems) //an Order has many OrderItems
                .HasForeignKey(oi => oi.Order);
            //Foreign key relationship with Status
            modelBuilder.Entity<OrderItem>()
                .HasOne<Status>(oi => oi.StatusNav) //has a Status
                .WithMany(s => s.OrderItems) //a Status has many OrderItems
                .HasForeignKey(oi => oi.Status)
                .OnDelete(DeleteBehavior.Restrict);

            //--Entity Order
            modelBuilder.Entity<Order>().HasKey(o => o.OrderId);
            modelBuilder.Entity<Order>().Property(o => o.OrderId).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.DeliveryTo).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Order>().Property(o => o.Notes).HasMaxLength(500);
            modelBuilder.Entity<Order>().Property(o => o.Price).IsRequired().HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().Property(o => o.CreateDate).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.UpdateDate).IsRequired();
            //Foreign key relationship with DeliveryType
            modelBuilder.Entity<Order>()
                .HasOne<DeliveryType>(o => o.DeliveryTypeNav) //has a DeliveryType
                .WithMany(dt => dt.Orders) //a DeliveryType has many Orders
                .HasForeignKey(o => o.DeliveryType);
            //Foreign key relationship with Status
            modelBuilder.Entity<Order>()
                .HasOne<Status>(o => o.StatusNav) //has a Status
                .WithMany(s => s.Orders) //a Status has many Orders
                .HasForeignKey(o => o.OverallStatus)
                 .OnDelete(DeleteBehavior.Restrict);

            //--Entity Status
            modelBuilder.Entity<Status>().HasKey(s => s.Id);
            modelBuilder.Entity<Status>().Property(s => s.Id).IsRequired();
            modelBuilder.Entity<Status>().Property(s => s.Name).IsRequired().HasMaxLength(25);

        }
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // Precarga Categories
            if (!context.Category.Any())
            {
                context.Category.AddRange(
                    new Category { Name = "Entradas", Description = "Pequeñas porciones para abrir el apetito antes del plato principal.", Order = 1 },
                    new Category { Name = "Ensaladas", Description = "Opciones frescas y livianas.", Order = 2 },
                    new Category { Name = "Minutas", Description = "Platos rápidos y clásicos de bodegón: milanesas, tortillas, revueltos.", Order = 3 },
                    new Category { Name = "Pastas", Description = "Variedad de pastas caseras y salsas tradicionales.", Order = 5 },
                    new Category { Name = "Parrilla", Description = "Cortes de carne asados a la parrilla, servidos con guarniciones.", Order = 4 },
                    new Category { Name = "Pizzas", Description = "Pizzas artesanales con masa casera y variedad de ingredientes.", Order = 7 },
                    new Category { Name = "Sandwiches", Description = "Sandwiches y lomitos completos preparados al momento.", Order = 6 },
                    new Category { Name = "Bebidas", Description = "Gaseosas, jugos, aguas y opciones sin alcohol.", Order = 8 },
                    new Category { Name = "Cerveza Artesanal", Description = "Cervezas de producción artesanal, rubias, rojas y negras.", Order = 9 },
                    new Category { Name = "Postres", Description = "Clásicos dulces caseros para cerrar la comida.", Order = 10 }
                );
                await context.SaveChangesAsync();
            }

            // Precarga DeliveryType
            if (!context.DeliveryType.Any())
            {
                context.DeliveryType.AddRange(
                    new DeliveryType { Name = "Delivery" },
                    new DeliveryType { Name = "Take away" },
                    new DeliveryType { Name = "Dine in" }
                );
                await context.SaveChangesAsync();
            }

            // Precarga Status
            if (!context.Status.Any())
            {
                context.Status.AddRange(
                    new Status { Name = "Pending" },
                    new Status { Name = "In progress" },
                    new Status { Name = "Ready" },
                    new Status { Name = "Delivery" },
                    new Status { Name = "Closed" }
                );
                await context.SaveChangesAsync();
            }
        }


    }
}
