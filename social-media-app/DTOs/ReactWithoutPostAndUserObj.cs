using social_media_app.Models;

namespace social_media_app.DTO
{
    public class ReactWithoutPostAndUserObj
    {
        public int Id { get; set; }
        public React_Value Value { get; set; }
        public string UserId { get; set; }
        public int PostId { get; set; }

    }

}
