using social_media_app.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.DTOs
{
    public class CommentDTO
    {
        public string Content { get; set; }

        public DateTime? CommentTime { get; set; }

        
        public int PostId { get; set; }

        
        public string UserId { get; set; }
    }
}
