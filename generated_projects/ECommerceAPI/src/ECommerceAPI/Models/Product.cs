using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal? ComparePrice { get; set; }
        [Required]
        [MaxLength(50)]
        public string SKU { get; set; }
        [MaxLength(50)]
        public string Barcode { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public int MinStock { get; set; }
        public decimal? Weight { get; set; }
        [MaxLength(255)]
        public string ImageUrl { get; set; }
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool IsFeatured { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}