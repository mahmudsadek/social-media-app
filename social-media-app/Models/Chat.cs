using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.Models
{
    public class Chat
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string SenderId { get; set; }
        public List<User> Users { get; set; }

        [ForeignKey("Message")]
        public int? LastMessageId { get; set; }

        [ForeignKey("User")]
        public List<string> ReceiversIds { get; set; } //More than one in case of grouping

        public List<Message>? Messages { get; set; }

    }
}
