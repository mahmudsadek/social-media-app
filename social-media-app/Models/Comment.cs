using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public DateTime CommentTime { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }

        public Post Post { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        
        public User User { get; set; }

        public IList<Replay>? Replays { get; set; }
            
    }
}