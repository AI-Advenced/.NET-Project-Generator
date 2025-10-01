using System.Data.Entity.ModelConfiguration;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Configurations
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            // Table name
            ToTable("Products");
            
            // Primary key
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            
            // Name configuration
            Property(x => x.Name).HasMaxLength(200);
            Property(x => x.Name).IsRequired();
            
            // Description configuration
            Property(x => x.Description).HasMaxLength(1000);
            Property(x => x.Price).IsRequired();
            
            // SKU configuration
            Property(x => x.SKU).HasMaxLength(50);
            Property(x => x.SKU).IsRequired();
            
            // Barcode configuration
            Property(x => x.Barcode).HasMaxLength(50);
            Property(x => x.Stock).IsRequired();
            Property(x => x.MinStock).IsRequired();
            
            // ImageUrl configuration
            Property(x => x.ImageUrl).HasMaxLength(255);
            Property(x => x.CategoryId).IsRequired();
            Property(x => x.IsActive).IsRequired();
            Property(x => x.IsFeatured).IsRequired();
            Property(x => x.CreatedDate).IsRequired();
        }
    }
}