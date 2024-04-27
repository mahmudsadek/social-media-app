using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using social_media_app.Models;

namespace social_media_app.DBContext
{
    public class Context: IdentityDbContext<User>
    {
        public DbSet<User> User { get; set; }
        
        public DbSet<Post> Post { get; set; }

        public DbSet<Comment> Comment { get; set; } 

        public DbSet<React> React { get; set; }

        public DbSet<Replay> Replay { get; set; }
    }
}
