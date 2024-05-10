using social_media_app.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.DTOs
{
    public class ChatDTO
    {
        public string ChatName { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public List<Message>? SenderTextMessages { get; set; }
        public List<Message>? ReceiverTextMessages { get; set; }
    }
}
