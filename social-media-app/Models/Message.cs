using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.Models
{
    public class Message
    {
        public int Id { get; set; }
        [ForeignKey("Chat")]
        public int ChatId { get; set; }
        [ForeignKey("User")]
        public string SenderMessageId { get; set; }
        public User User { get; set; }

        public DateTime SenderMessageTime { get; set; }
        public string SenderMessageText { get; set; }

        public DateTime ReceiverMessagesTime { get; set; }
        public string ReceiverMessageText { get; set; }

        public bool MessageSeen {  get; set; } = false;

        public Chat chat { get; set; }

    }
}
