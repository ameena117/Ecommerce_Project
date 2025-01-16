using Ecommerce_Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Infrastracture.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("AspNetUsers");

            //modelBuilder.Entity<CartItem>()
            //    .HasKey(ci => new { ci.ProductId, ci.CartId }); // Composite key

            //modelBuilder.Entity<OrderItem>()
            //  .HasKey(oi => new { oi.ProductId, oi.OrderId }); // Composite key

            //modelBuilder.Entity<CustomerStores>()
            //  .HasKey(cs => new { cs.Cus_Id, cs.StoreId }); // Composite key

            //modelBuilder.Entity<ShoppingCartItem>()
            // .HasKey(sc => new { sc.Cus_Id, sc.Store_Id, sc.ItemCode }); // Composite key

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProducts> CartProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
