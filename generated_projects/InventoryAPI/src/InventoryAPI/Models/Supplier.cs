using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAPI.Models
{
    public class Supplier
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string CompanyName { get; set; }
        [Required]
        [MaxLength(100)]
        public string ContactName { get; set; }
        [Required]
        [MaxLength(100)]
        public string ContactEmail { get; set; }
        [MaxLength(20)]
        public string ContactPhone { get; set; }
        [MaxLength(500)]
        public string Address { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(50)]
        public string State { get; set; }
        [MaxLength(10)]
        public string ZipCode { get; set; }
        [MaxLength(50)]
        public string Country { get; set; }
        [MaxLength(255)]
        public string Website { get; set; }
        [MaxLength(50)]
        public string TaxId { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}