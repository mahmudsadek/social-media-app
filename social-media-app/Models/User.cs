using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace social_media_app.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Name Must Be Less Than 20 Char")]
        [MinLength(3, ErrorMessage = "Name Must Be More That 3 Char")]
        public string? Name { get; set; } = "sa";

        public string? ProfileImage { get; set; } = "dss";

        public string? CoverImage { get; set; } = "dssd";

        [MaxLength(70, ErrorMessage = "Bio Must Be Less Than 70 Char")]
        [MinLength(10, ErrorMessage = "Name Must Be More That 10 Char")]
        public string? Bio { get; set; } = "";

        public string? Location { get; set; } = "";

        public DateTime JoinDate { get; set; } = DateTime.Now;

        public IList<Post>? Posts { get; set; }

        public IList<Comment>? Commnets {  get; set; }

        public IList<React>? Reacts {  get; set; }

        public IList<Chat>? Chats { get; set; }


        public IList<User>? Followers { get; set; }

        public List<User>? PeopleIFollow { get; set; }
    }
}
