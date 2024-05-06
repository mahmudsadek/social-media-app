using Microsoft.AspNetCore.Mvc;
using social_media_app.Repository;
using System.Runtime.Intrinsics.X86;
using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public DateTime? PostTime { get; set; }

        public string? PostImage { get; set; }

        public string UserId { get; set; }

        public int? LikeCount { get; set; }
        public int? DislikeCount { get; set; }
    }
}
