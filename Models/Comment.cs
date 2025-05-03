using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BookNookWebApp.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        // Update the property name to PostedAt
        public DateTime PostedAt { get; set; } = DateTime.UtcNow;

        // Foreign key to IdentityUser (author)
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        // Foreign key to ForumPost
        public int ForumPostId { get; set; }
        [ForeignKey("ForumPostId")]
        public ForumPost ForumPost { get; set; }
    }
}
