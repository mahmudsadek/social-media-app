using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace social_media_app.Models
{
    public class Notify
    {
        public int Id { get; set; }
        public string Content { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }


        [ForeignKey("PostedUser")]
        public string PostedUserId { get; set; }
        [JsonIgnore]
        public User PostedUser { get; set; }
    }
}
