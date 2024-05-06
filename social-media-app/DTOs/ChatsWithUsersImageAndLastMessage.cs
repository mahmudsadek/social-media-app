using social_media_app.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.DTOs
{
    public class ChatsWithUsersImageAndLastMessage
    {
        public int Id { get; set; }
        public string SenderName { get; set; }

        public string SenderImage { get; set; }

        public DateTime? MessageTime { get; set; }

        public string? LastMessageText { get; set; }


    }
}
