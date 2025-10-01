using System.Data.Entity.ModelConfiguration;
using BlogAPI.Models;

namespace BlogAPI.Data.Configurations
{
    public class BlogPostConfiguration : EntityTypeConfiguration<BlogPost>
    {
        public BlogPostConfiguration()
        {
            // Table name
            ToTable("BlogPosts");
            
            // Primary key
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            
            // Title configuration
            Property(x => x.Title).HasMaxLength(200);
            Property(x => x.Title).IsRequired();
            
            // Slug configuration
            Property(x => x.Slug).HasMaxLength(250);
            Property(x => x.Slug).IsRequired();
            Property(x => x.Content).IsRequired();
            
            // Excerpt configuration
            Property(x => x.Excerpt).HasMaxLength(500);
            
            // FeaturedImageUrl configuration
            Property(x => x.FeaturedImageUrl).HasMaxLength(255);
            Property(x => x.AuthorId).IsRequired();
            
            // Status configuration
            Property(x => x.Status).HasMaxLength(20);
            Property(x => x.Status).IsRequired();
            Property(x => x.IsCommentEnabled).IsRequired();
            Property(x => x.IsFeatured).IsRequired();
            
            // MetaTitle configuration
            Property(x => x.MetaTitle).HasMaxLength(200);
            
            // MetaDescription configuration
            Property(x => x.MetaDescription).HasMaxLength(300);
            Property(x => x.CreatedDate).IsRequired();
        }
    }
}