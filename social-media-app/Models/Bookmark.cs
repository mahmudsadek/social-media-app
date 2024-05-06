using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.Models
{
    public class Bookmark
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        
        public string userId { get; set; }

        public User User { get; set; }

        public List<Post>? posts { get; set; }
    }
}
