using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookNookWebApp.Models
{
    public class Topics
    {
        [Key]
        public int TopicId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // Navigation property for forum posts in this topic
        public ICollection<ForumPost> ForumPosts { get; set; } = new List<ForumPost>();
    }
}
