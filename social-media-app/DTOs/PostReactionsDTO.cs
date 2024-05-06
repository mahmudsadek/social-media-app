namespace social_media_app.DTOs
{
    public class PostReactionsDTO
    {
        public int PostId { get; set; }
        public int? LikeCount { get; set; }
        public int? DislikeCount { get; set; }
    }
}
