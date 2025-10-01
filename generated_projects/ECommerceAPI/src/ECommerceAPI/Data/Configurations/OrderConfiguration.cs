using System.Data.Entity.ModelConfiguration;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Configurations
{
    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            // Table name
            ToTable("Orders");
            
            // Primary key
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            
            // OrderNumber configuration
            Property(x => x.OrderNumber).HasMaxLength(20);
            Property(x => x.OrderNumber).IsRequired();
            Property(x => x.UserId).IsRequired();
            
            // Status configuration
            Property(x => x.Status).HasMaxLength(20);
            Property(x => x.Status).IsRequired();
            Property(x => x.SubTotal).IsRequired();
            Property(x => x.TaxAmount).IsRequired();
            Property(x => x.ShippingAmount).IsRequired();
            Property(x => x.TotalAmount).IsRequired();
            
            // ShippingAddress configuration
            Property(x => x.ShippingAddress).HasMaxLength(500);
            Property(x => x.ShippingAddress).IsRequired();
            
            // BillingAddress configuration
            Property(x => x.BillingAddress).HasMaxLength(500);
            Property(x => x.BillingAddress).IsRequired();
            
            // PaymentMethod configuration
            Property(x => x.PaymentMethod).HasMaxLength(50);
            Property(x => x.PaymentMethod).IsRequired();
            
            // PaymentStatus configuration
            Property(x => x.PaymentStatus).HasMaxLength(20);
            Property(x => x.PaymentStatus).IsRequired();
            
            // Notes configuration
            Property(x => x.Notes).HasMaxLength(1000);
            Property(x => x.CreatedDate).IsRequired();
        }
    }
}