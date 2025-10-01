using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{
    public class Comment
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [ForeignKey("BlogPost")]
        public int BlogPostId { get; set; }
        [ForeignKey("Comment")]
        public int? ParentCommentId { get; set; }
        [Required]
        [MaxLength(100)]
        public string AuthorName { get; set; }
        [Required]
        [MaxLength(100)]
        public string AuthorEmail { get; set; }
        [MaxLength(255)]
        public string AuthorWebsite { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }
        [Required]
        public bool IsApproved { get; set; }
        [MaxLength(45)]
        public string IpAddress { get; set; }
        [MaxLength(500)]
        public string UserAgent { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
    }
}