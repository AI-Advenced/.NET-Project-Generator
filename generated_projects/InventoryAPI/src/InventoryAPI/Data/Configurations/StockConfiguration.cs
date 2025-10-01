using System.Data.Entity.ModelConfiguration;
using InventoryAPI.Models;

namespace InventoryAPI.Data.Configurations
{
    public class StockConfiguration : EntityTypeConfiguration<Stock>
    {
        public StockConfiguration()
        {
            // Table name
            ToTable("Stocks");
            
            // Primary key
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.ItemId).IsRequired();
            Property(x => x.WarehouseId).IsRequired();
            Property(x => x.QuantityOnHand).IsRequired();
            Property(x => x.QuantityReserved).IsRequired();
            Property(x => x.QuantityAvailable).IsRequired();
            
            // Location configuration
            Property(x => x.Location).HasMaxLength(50);
            Property(x => x.CreatedDate).IsRequired();
        }
    }
}