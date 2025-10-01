using System.Data.Entity;
using InventoryAPI.Models;

namespace InventoryAPI.Data
{
    public class InventoryAPIContext : DbContext
    {
        public InventoryAPIContext() : base("DefaultConnection")
        {
        }
        
        public DbSet<Supplier> Suppliers { get; set; }
        
        public DbSet<Warehouse> Warehouses { get; set; }
        
        public DbSet<Item> Items { get; set; }
        
        public DbSet<Stock> Stocks { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Configurations.Add(new Configurations.SupplierConfiguration());
            
            modelBuilder.Configurations.Add(new Configurations.WarehouseConfiguration());
            
            modelBuilder.Configurations.Add(new Configurations.ItemConfiguration());
            
            modelBuilder.Configurations.Add(new Configurations.StockConfiguration());
        }
    }
}