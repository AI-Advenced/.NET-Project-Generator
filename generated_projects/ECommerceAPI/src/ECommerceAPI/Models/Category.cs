using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(255)]
        public string ImageUrl { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}