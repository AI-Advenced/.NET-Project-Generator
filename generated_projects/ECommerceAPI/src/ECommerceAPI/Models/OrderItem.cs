using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.Models
{
    public class OrderItem
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; }
        [Required]
        [MaxLength(50)]
        public string ProductSKU { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
    }
}