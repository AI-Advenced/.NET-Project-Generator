using System.Data.Entity;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data
{
    public class ECommerceAPIContext : DbContext
    {
        public ECommerceAPIContext() : base("DefaultConnection")
        {
        }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<Category> Categorys { get; set; }
        
        public DbSet<Product> Products { get; set; }
        
        public DbSet<Order> Orders { get; set; }
        
        public DbSet<OrderItem> OrderItems { get; set; }
        
        public DbSet<Review> Reviews { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Configurations.Add(new Configurations.UserConfiguration());
            
            modelBuilder.Configurations.Add(new Configurations.CategoryConfiguration());
            
            modelBuilder.Configurations.Add(new Configurations.ProductConfiguration());
            
            modelBuilder.Configurations.Add(new Configurations.OrderConfiguration());
            
            modelBuilder.Configurations.Add(new Configurations.OrderItemConfiguration());
            
            modelBuilder.Configurations.Add(new Configurations.ReviewConfiguration());
        }
    }
}