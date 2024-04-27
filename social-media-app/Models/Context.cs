using Microsoft.EntityFrameworkCore;

namespace social_media_app.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// for default data in database like this ^_^

            //modelBuilder.Entity<Product>()
            //    .HasData(new Product() { Id = 2, Name = "car", Description = "Expinsive one", Price = 20000, Quentity = 10 });

        }
    }
}
