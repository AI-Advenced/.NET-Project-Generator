using System.Data.Entity.ModelConfiguration;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Configurations
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            // Table name
            ToTable("Categorys");
            
            // Primary key
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            
            // Name configuration
            Property(x => x.Name).HasMaxLength(100);
            Property(x => x.Name).IsRequired();
            
            // Description configuration
            Property(x => x.Description).HasMaxLength(500);
            
            // ImageUrl configuration
            Property(x => x.ImageUrl).HasMaxLength(255);
            Property(x => x.IsActive).IsRequired();
            Property(x => x.CreatedDate).IsRequired();
        }
    }
}