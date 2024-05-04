using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.Models
{
    public class Chat
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int SenderId { get; set; }

        [ForeignKey("User")]
        public int ReceiverId { get; set; }
        public List<Message> Messages { get; set; }
    }
}
