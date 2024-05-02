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





        public Context(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //// for default data in database like this ^_^

        //    //modelBuilder.Entity<Product>()
        //    //    .HasData(new Product() { Id = 2, Name = "car", Description = "Expinsive one", Price = 20000, Quentity = 10 });

        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>().HasData(
                new Post { Id = 1, Content = "First post", PostTime = DateTime.Now, UserId = "user1", LoveCount = 0, CommentCount = 0, ShareCount = 0 },
                new Post { Id = 2, Content = "Second post", PostTime = DateTime.Now, UserId = "user2", LoveCount = 0, CommentCount = 0, ShareCount = 0 }
            );
        }

    }

    
}
