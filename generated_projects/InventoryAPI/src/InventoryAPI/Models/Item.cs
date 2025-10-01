using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAPI.Models
{
    public class Item
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
        [MaxLength(50)]
        public string SKU { get; set; }
        [MaxLength(50)]
        public string Barcode { get; set; }
        [MaxLength(100)]
        public string Category { get; set; }
        [MaxLength(100)]
        public string Brand { get; set; }
        [Required]
        [MaxLength(20)]
        public string UnitOfMeasure { get; set; }
        [Required]
        public decimal UnitCost { get; set; }
        [Required]
        public decimal SalePrice { get; set; }
        public decimal? Weight { get; set; }
        [MaxLength(100)]
        public string Dimensions { get; set; }
        [Required]
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        [Required]
        public int MinStockLevel { get; set; }
        [Required]
        public int MaxStockLevel { get; set; }
        [Required]
        public int ReorderPoint { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}