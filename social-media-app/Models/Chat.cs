using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.Models
{
    public class Chat
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string SenderId { get; set; }

        [ForeignKey("User")]
        public string ReceiverId { get; set; }

        [ForeignKey("Message")]
        public List<int>? MessagesId { get; set; }
        public List<Message>? Messages { get; set; }

    }
}
