using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public DateTime PostTime { get; set; } = DateTime.Now;

        public string? PostImage { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public User User { get; set; }

        public int LoveCount { get; set; } = 0;

        public int CommentCount { get; set; } = 0;

        public int ShareCount { get; set; } = 0;
        
        public IList<React>? Reactions { get; set; }
        
        public IList<Comment>? Comments { get; set; }
    }
}