using System.Data.Entity.ModelConfiguration;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Configurations
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
            
            // FirstName configuration
            Property(x => x.FirstName).HasMaxLength(50);
            Property(x => x.FirstName).IsRequired();
            
            // LastName configuration
            Property(x => x.LastName).HasMaxLength(50);
            Property(x => x.LastName).IsRequired();
            
            // PasswordHash configuration
            Property(x => x.PasswordHash).HasMaxLength(255);
            Property(x => x.PasswordHash).IsRequired();
            
            // PhoneNumber configuration
            Property(x => x.PhoneNumber).HasMaxLength(20);
            Property(x => x.CreatedDate).IsRequired();
            Property(x => x.IsActive).IsRequired();
            Property(x => x.IsEmailConfirmed).IsRequired();
        }
    }
}