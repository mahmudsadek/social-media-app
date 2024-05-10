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

        public DbSet<Notify> Notify { get; set; } 


        public DbSet<React> React { get; set; }

        public DbSet<Replay> Replay { get; set; }

        public DbSet<UserFollower> UserFollower { get; set; }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

       



        public Context(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //// for default data in database like this ^_^

        //    //modelBuilder.Entity<Product>()
        //    //    .HasData(new Product() { Id = 2, Name = "car", Description = "Expinsive one", Price = 20000, Quentity = 10 });

        //}


    }

    
}
