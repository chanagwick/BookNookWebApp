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

        
       public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
