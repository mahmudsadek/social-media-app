using social_media_app.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.DTOs
{
    public class ChatDTO
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public List<string> ReceiversIds { get; set; }
        public List<string>? SenderMessages { get; set; }
        public List<string>? ReceiverMessages { get; set; }
    }
}
