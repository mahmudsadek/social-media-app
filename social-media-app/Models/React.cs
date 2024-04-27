using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.Models
{
    public class React
    {
        public int Id { get; set; }
        public bool Value {  get; set; }

        [ForeignKey("User")]
        public int UserId {  get; set; }
        
        public User User { get; set; }


        [ForeignKey("Post")]
        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}