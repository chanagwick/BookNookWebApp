using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BookNookWebApp.Models
{
    public class ForumPost
    {
        [Key]
        public int ForumPostId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // New PostedAt property
        public DateTime PostedAt { get; set; } = DateTime.UtcNow; // Add this line

        // Foreign key to IdentityUser (author)
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        // Foreign key to Topic
        public int TopicId { get; set; }
        [ForeignKey("TopicId")]
        public Topics Topic { get; set; }

        // Collection of Comments related to the ForumPost
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
