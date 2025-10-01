using System.Data.Entity.ModelConfiguration;
using BlogAPI.Models;

namespace BlogAPI.Data.Configurations
{
    public class AuthorConfiguration : EntityTypeConfiguration<Author>
    {
        public AuthorConfiguration()
        {
            // Table name
            ToTable("Authors");
            
            // Primary key
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            
            // Email configuration
            Property(x => x.Email).HasMaxLength(100);
            Property(x => x.Email).IsRequired();
            
            // Username configuration
            Property(x => x.Username).HasMaxLength(50);
            Property(x => x.Username).IsRequired();
            
            // FirstName configuration
            Property(x => x.FirstName).HasMaxLength(50);
            Property(x => x.FirstName).IsRequired();
            
            // LastName configuration
            Property(x => x.LastName).HasMaxLength(50);
            Property(x => x.LastName).IsRequired();
            
            // Bio configuration
            Property(x => x.Bio).HasMaxLength(1000);
            
            // AvatarUrl configuration
            Property(x => x.AvatarUrl).HasMaxLength(255);
            
            // Website configuration
            Property(x => x.Website).HasMaxLength(255);
            
            // TwitterHandle configuration
            Property(x => x.TwitterHandle).HasMaxLength(50);
            Property(x => x.IsActive).IsRequired();
            Property(x => x.CreatedDate).IsRequired();
        }
    }
}