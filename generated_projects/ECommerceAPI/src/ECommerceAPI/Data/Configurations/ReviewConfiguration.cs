using System.Data.Entity.ModelConfiguration;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Configurations
{
    public class ReviewConfiguration : EntityTypeConfiguration<Review>
    {
        public ReviewConfiguration()
        {
            // Table name
            ToTable("Reviews");
            
            // Primary key
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.ProductId).IsRequired();
            Property(x => x.UserId).IsRequired();
            Property(x => x.Rating).IsRequired();
            
            // Title configuration
            Property(x => x.Title).HasMaxLength(200);
            Property(x => x.Title).IsRequired();
            
            // Comment configuration
            Property(x => x.Comment).HasMaxLength(2000);
            Property(x => x.IsApproved).IsRequired();
            Property(x => x.CreatedDate).IsRequired();
        }
    }
}