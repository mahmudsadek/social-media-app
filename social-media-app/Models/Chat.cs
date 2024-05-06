using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string SenderId { get; set; }

        [ForeignKey("Message")]
        public int? LastMessageId { get; set; }

        [ForeignKey("ReceiverUser")]
        public string ReceiverId { get; set; }
        public User ReceiverUser { get; set; }
        public List<Message>? Messages { get; set; }

    }
}
