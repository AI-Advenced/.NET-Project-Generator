using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{
    public class Tag
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(60)]
        public string Slug { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(7)]
        public string Color { get; set; }
        public int? PostCount { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
    }
}