//using Microsoft.EntityFrameworkCore;

//namespace social_media_app.Models
//{
//    public class PostContext : DbContext
//    {
//        public DbSet<Post> Posts { get; set; }

//        public PostContext(DbContextOptions<PostContext> options) : base(options)
//        {
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            modelBuilder.Entity<Post>().HasData(
//                new Post { Id = 1, Content = "First post", PostTime = DateTime.Now, UserId = "user1", LoveCount = 0, CommentCount = 0, ShareCount = 0 },
//                new Post { Id = 2, Content = "Second post", PostTime = DateTime.Now, UserId = "user2", LoveCount = 0, CommentCount = 0, ShareCount = 0 }
//            );
//        }

//    }
//}
