using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.Models
{
    public class Order
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string OrderNumber { get; set; }
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        [MaxLength(20)]
        public string Status { get; set; }
        [Required]
        public decimal SubTotal { get; set; }
        [Required]
        public decimal TaxAmount { get; set; }
        [Required]
        public decimal ShippingAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        [Required]
        [MaxLength(500)]
        public string ShippingAddress { get; set; }
        [Required]
        [MaxLength(500)]
        public string BillingAddress { get; set; }
        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; }
        [Required]
        [MaxLength(20)]
        public string PaymentStatus { get; set; }
        [MaxLength(1000)]
        public string Notes { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
    }
}