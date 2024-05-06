using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.Models
{
    public class Replay
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public DateTime ReplayTime { get; set; } = DateTime.Now;

        [ForeignKey("Comment")]
        public int CommentId { get; set; }

        public Comment Comment { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }

        public Post Post { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}