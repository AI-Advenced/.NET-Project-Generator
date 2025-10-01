using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.Models
{
    public class Review
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(2000)]
        public string Comment { get; set; }
        [Required]
        public bool IsApproved { get; set; }
        public int? IsHelpful { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}