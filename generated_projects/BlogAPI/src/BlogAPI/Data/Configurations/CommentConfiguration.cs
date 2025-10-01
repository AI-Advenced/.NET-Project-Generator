using System.Data.Entity.ModelConfiguration;
using BlogAPI.Models;

namespace BlogAPI.Data.Configurations
{
    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            // Table name
            ToTable("Comments");
            
            // Primary key
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.BlogPostId).IsRequired();
            
            // AuthorName configuration
            Property(x => x.AuthorName).HasMaxLength(100);
            Property(x => x.AuthorName).IsRequired();
            
            // AuthorEmail configuration
            Property(x => x.AuthorEmail).HasMaxLength(100);
            Property(x => x.AuthorEmail).IsRequired();
            
            // AuthorWebsite configuration
            Property(x => x.AuthorWebsite).HasMaxLength(255);
            
            // Content configuration
            Property(x => x.Content).HasMaxLength(2000);
            Property(x => x.Content).IsRequired();
            Property(x => x.IsApproved).IsRequired();
            
            // IpAddress configuration
            Property(x => x.IpAddress).HasMaxLength(45);
            
            // UserAgent configuration
            Property(x => x.UserAgent).HasMaxLength(500);
            Property(x => x.CreatedDate).IsRequired();
        }
    }
}