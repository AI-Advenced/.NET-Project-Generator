using System.Data.Entity.ModelConfiguration;
using BasicCrudAPI.Models;

namespace BasicCrudAPI.Data.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            // Table name
            ToTable("Users");
            
            // Primary key
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            
            // Email configuration
            Property(x => x.Email).HasMaxLength(100);
            Property(x => x.Email).IsRequired();
            
            // Name configuration
            Property(x => x.Name).HasMaxLength(50);
            Property(x => x.Name).IsRequired();
            Property(x => x.CreatedDate).IsRequired();
        }
    }
}