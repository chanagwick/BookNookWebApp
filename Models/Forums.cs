using System.ComponentModel.DataAnnotations;

namespace BookNookWebApp.Models
{
    public class Forums
    {
        [Key]public int ForumID { get; set; }

        public string Name { get; set; }


        public string Description { get; set; }


        public Forums()
        {
            
        }
    }
}
