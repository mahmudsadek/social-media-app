using social_media_app.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.DTOs
{
    public class MessageDTO
    {
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        public DateTime SenderMessageTime { get; set; }
        public string SenderMessage { get; set; }
        public DateTime ReceiverMessagesTime { get; set; }
        public string ReceiverMessage { get; set; }

    }
}
