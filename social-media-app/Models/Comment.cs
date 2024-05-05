using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace social_media_app.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }

        public DateTime CommentTime { get; set; } = DateTime.Now;

        [ForeignKey("Post")]
        public int PostId { get; set; }
        [JsonIgnore]
        public Post Post { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public IList<Replay>? Replays { get; set; }
            
    }
}