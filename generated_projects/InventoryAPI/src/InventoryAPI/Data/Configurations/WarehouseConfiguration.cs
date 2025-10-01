using System.Data.Entity.ModelConfiguration;
using InventoryAPI.Models;

namespace InventoryAPI.Data.Configurations
{
    public class WarehouseConfiguration : EntityTypeConfiguration<Warehouse>
    {
        public WarehouseConfiguration()
        {
            // Table name
            ToTable("Warehouses");
            
            // Primary key
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            
            // Name configuration
            Property(x => x.Name).HasMaxLength(100);
            Property(x => x.Name).IsRequired();
            
            // Code configuration
            Property(x => x.Code).HasMaxLength(10);
            Property(x => x.Code).IsRequired();
            
            // Address configuration
            Property(x => x.Address).HasMaxLength(500);
            Property(x => x.Address).IsRequired();
            
            // City configuration
            Property(x => x.City).HasMaxLength(100);
            Property(x => x.City).IsRequired();
            
            // State configuration
            Property(x => x.State).HasMaxLength(50);
            Property(x => x.State).IsRequired();
            
            // ZipCode configuration
            Property(x => x.ZipCode).HasMaxLength(10);
            Property(x => x.ZipCode).IsRequired();
            
            // Country configuration
            Property(x => x.Country).HasMaxLength(50);
            Property(x => x.Country).IsRequired();
            
            // ManagerName configuration
            Property(x => x.ManagerName).HasMaxLength(100);
            
            // ManagerEmail configuration
            Property(x => x.ManagerEmail).HasMaxLength(100);
            
            // ManagerPhone configuration
            Property(x => x.ManagerPhone).HasMaxLength(20);
            Property(x => x.IsActive).IsRequired();
            Property(x => x.CreatedDate).IsRequired();
        }
    }
}