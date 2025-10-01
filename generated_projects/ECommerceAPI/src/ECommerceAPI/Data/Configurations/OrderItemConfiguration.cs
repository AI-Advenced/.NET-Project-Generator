using System.Data.Entity.ModelConfiguration;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Configurations
{
    public class OrderItemConfiguration : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemConfiguration()
        {
            // Table name
            ToTable("OrderItems");
            
            // Primary key
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.OrderId).IsRequired();
            Property(x => x.ProductId).IsRequired();
            
            // ProductName configuration
            Property(x => x.ProductName).HasMaxLength(200);
            Property(x => x.ProductName).IsRequired();
            
            // ProductSKU configuration
            Property(x => x.ProductSKU).HasMaxLength(50);
            Property(x => x.ProductSKU).IsRequired();
            Property(x => x.UnitPrice).IsRequired();
            Property(x => x.Quantity).IsRequired();
            Property(x => x.TotalPrice).IsRequired();
            Property(x => x.CreatedDate).IsRequired();
        }
    }
}