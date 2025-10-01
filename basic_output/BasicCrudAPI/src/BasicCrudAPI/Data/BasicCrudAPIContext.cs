using System.Data.Entity;
using BasicCrudAPI.Models;

namespace BasicCrudAPI.Data
{
    public class BasicCrudAPIContext : DbContext
    {
        public BasicCrudAPIContext() : base("DefaultConnection")
        {
        }
        
        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Configurations.Add(new Configurations.UserConfiguration());
        }
    }
}