using System.Data.Entity.ModelConfiguration;
using BlogAPI.Models;

namespace BlogAPI.Data.Configurations
{
    public class TagConfiguration : EntityTypeConfiguration<Tag>
    {
        public TagConfiguration()
        {
            // Table name
            ToTable("Tags");
            
            // Primary key
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            
            // Name configuration
            Property(x => x.Name).HasMaxLength(50);
            Property(x => x.Name).IsRequired();
            
            // Slug configuration
            Property(x => x.Slug).HasMaxLength(60);
            Property(x => x.Slug).IsRequired();
            
            // Description configuration
            Property(x => x.Description).HasMaxLength(500);
            
            // Color configuration
            Property(x => x.Color).HasMaxLength(7);
            Property(x => x.CreatedDate).IsRequired();
        }
    }
}