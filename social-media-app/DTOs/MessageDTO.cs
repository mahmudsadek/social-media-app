using social_media_app.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.DTOs
{
    public class MessageDTO
    {
        public int MessagesId { get; set; }
        public int ChatId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime SenderMessageTime { get; set; }
        public string SenderMessageContent { get; set; }
        public DateTime ReceiverMessagesTime { get; set; }
        public string ReceiverMessageContent { get; set; }
    }
}
