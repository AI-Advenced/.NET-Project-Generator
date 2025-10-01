using System.Data.Entity;
using BlogAPI.Models;

namespace BlogAPI.Data
{
    public class BlogAPIContext : DbContext
    {
        public BlogAPIContext() : base("DefaultConnection")
        {
        }
        
        public DbSet<Author> Authors { get; set; }
        
        public DbSet<BlogPost> BlogPosts { get; set; }
        
        public DbSet<Comment> Comments { get; set; }
        
        public DbSet<Tag> Tags { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Configurations.Add(new Configurations.AuthorConfiguration());
            
            modelBuilder.Configurations.Add(new Configurations.BlogPostConfiguration());
            
            modelBuilder.Configurations.Add(new Configurations.CommentConfiguration());
            
            modelBuilder.Configurations.Add(new Configurations.TagConfiguration());
        }
    }
}