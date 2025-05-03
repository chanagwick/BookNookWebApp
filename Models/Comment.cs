using System;
using System.Collections.Generic;
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

        public DateTime PostedAt { get; set; } = DateTime.UtcNow;

        // Foreign key to IdentityUser (author)
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        // Foreign key to Topic
        public int TopicId { get; set; }
        [ForeignKey("TopicId")]
        public Topics Topic { get; set; }

        // Support for replies
        public int? ParentCommentId { get; set; }
        [ForeignKey("ParentCommentId")]
        public Comment ParentComment { get; set; }

        // Replies collection for a comment
        public ICollection<Comment> Replies { get; set; } = new List<Comment>();
    }
}
