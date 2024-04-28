
using Microsoft.EntityFrameworkCore;
using social_media_app.DBContext;
using social_media_app.Models;

namespace social_media_app
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer("Data Source=.;Initial Catalog=Social_Media_Api;trustservercertificate = true;Integrated Security=True;Encrypt=False");
            });

            builder.Services.AddCors(options => options.AddPolicy("MyPolicy", policy => 
            policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("MyPolicy");

            app.UseAuthorization();

            

            app.MapControllers();

            app.Run();
        }
    }
}
