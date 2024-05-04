using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.Models
{
    public class Message
    {
        public int Id { get; set; }
        [ForeignKey("Chat")]
        public int ChatId { get; set; }

        public DateTime SenderMessageTime { get; set; }
        public string SenderMessage { get; set; }

        public DateTime ReceiverMessagesTime { get; set; }
        public string ReceiverMessage { get; set; }

    }
}
