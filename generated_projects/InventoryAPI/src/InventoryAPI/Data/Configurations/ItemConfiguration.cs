using System.Data.Entity.ModelConfiguration;
using InventoryAPI.Models;

namespace InventoryAPI.Data.Configurations
{
    public class ItemConfiguration : EntityTypeConfiguration<Item>
    {
        public ItemConfiguration()
        {
            // Table name
            ToTable("Items");
            
            // Primary key
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            
            // Name configuration
            Property(x => x.Name).HasMaxLength(200);
            Property(x => x.Name).IsRequired();
            
            // Description configuration
            Property(x => x.Description).HasMaxLength(1000);
            
            // SKU configuration
            Property(x => x.SKU).HasMaxLength(50);
            Property(x => x.SKU).IsRequired();
            
            // Barcode configuration
            Property(x => x.Barcode).HasMaxLength(50);
            
            // Category configuration
            Property(x => x.Category).HasMaxLength(100);
            
            // Brand configuration
            Property(x => x.Brand).HasMaxLength(100);
            
            // UnitOfMeasure configuration
            Property(x => x.UnitOfMeasure).HasMaxLength(20);
            Property(x => x.UnitOfMeasure).IsRequired();
            Property(x => x.UnitCost).IsRequired();
            Property(x => x.SalePrice).IsRequired();
            
            // Dimensions configuration
            Property(x => x.Dimensions).HasMaxLength(100);
            Property(x => x.SupplierId).IsRequired();
            Property(x => x.MinStockLevel).IsRequired();
            Property(x => x.MaxStockLevel).IsRequired();
            Property(x => x.ReorderPoint).IsRequired();
            Property(x => x.IsActive).IsRequired();
            Property(x => x.CreatedDate).IsRequired();
        }
    }
}