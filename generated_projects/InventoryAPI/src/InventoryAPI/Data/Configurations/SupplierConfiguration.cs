using System.Data.Entity.ModelConfiguration;
using InventoryAPI.Models;

namespace InventoryAPI.Data.Configurations
{
    public class SupplierConfiguration : EntityTypeConfiguration<Supplier>
    {
        public SupplierConfiguration()
        {
            // Table name
            ToTable("Suppliers");
            
            // Primary key
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            
            // CompanyName configuration
            Property(x => x.CompanyName).HasMaxLength(200);
            Property(x => x.CompanyName).IsRequired();
            
            // ContactName configuration
            Property(x => x.ContactName).HasMaxLength(100);
            Property(x => x.ContactName).IsRequired();
            
            // ContactEmail configuration
            Property(x => x.ContactEmail).HasMaxLength(100);
            Property(x => x.ContactEmail).IsRequired();
            
            // ContactPhone configuration
            Property(x => x.ContactPhone).HasMaxLength(20);
            
            // Address configuration
            Property(x => x.Address).HasMaxLength(500);
            
            // City configuration
            Property(x => x.City).HasMaxLength(100);
            
            // State configuration
            Property(x => x.State).HasMaxLength(50);
            
            // ZipCode configuration
            Property(x => x.ZipCode).HasMaxLength(10);
            
            // Country configuration
            Property(x => x.Country).HasMaxLength(50);
            
            // Website configuration
            Property(x => x.Website).HasMaxLength(255);
            
            // TaxId configuration
            Property(x => x.TaxId).HasMaxLength(50);
            Property(x => x.IsActive).IsRequired();
            Property(x => x.CreatedDate).IsRequired();
        }
    }
}