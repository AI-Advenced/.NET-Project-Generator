using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool IsEmailConfirmed { get; set; }
    }
}