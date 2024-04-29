using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.Models
{
    public enum React_Value
    {
        Like,
        Dislike
    };

    // Refaey : I have changed the bool with enum to make it readable and to let us easy add others reaction if we want
    public class React
    {
        public int Id { get; set; }
        public bool Value { get; set; }

        [ForeignKey("User")]
        public string UserId {  get; set; }
        
        public User User { get; set; }


        [ForeignKey("Post")]
        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}