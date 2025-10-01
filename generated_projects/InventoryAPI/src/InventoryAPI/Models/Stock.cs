using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAPI.Models
{
    public class Stock
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Item")]
        public int ItemId { get; set; }
        [Required]
        [ForeignKey("Warehouse")]
        public int WarehouseId { get; set; }
        [Required]
        public int QuantityOnHand { get; set; }
        [Required]
        public int QuantityReserved { get; set; }
        [Required]
        public int QuantityAvailable { get; set; }
        [MaxLength(50)]
        public string Location { get; set; }
        public DateTime? LastCountDate { get; set; }
        public DateTime? LastMovementDate { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}