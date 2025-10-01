using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{
    public class BlogPost
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        [MaxLength(250)]
        public string Slug { get; set; }
        [Required]
        public string Content { get; set; }
        [MaxLength(500)]
        public string Excerpt { get; set; }
        [MaxLength(255)]
        public string FeaturedImageUrl { get; set; }
        [Required]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        [Required]
        [MaxLength(20)]
        public string Status { get; set; }
        public int? ViewCount { get; set; }
        public int? LikeCount { get; set; }
        [Required]
        public bool IsCommentEnabled { get; set; }
        [Required]
        public bool IsFeatured { get; set; }
        [MaxLength(200)]
        public string MetaTitle { get; set; }
        [MaxLength(300)]
        public string MetaDescription { get; set; }
        public DateTime? PublishedDate { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}