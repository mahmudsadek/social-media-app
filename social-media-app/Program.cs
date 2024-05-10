
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using social_media_app.DBContext;
using social_media_app.Models;
using social_media_app.Repository;
using social_media_app.Services;
using System.Text;

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

            builder.Services.AddScoped<IReactRepository, ReactRepository>();
            //builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<Context>();

            builder.Services.AddCors(options => options.AddPolicy("MyPolicy", policy => 
            policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));


            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IReplayRepository, ReplayRepository>();
            builder.Services.AddScoped<INotifyRepository, NotifyRepository>();
            builder.Services.AddScoped<IUserFollowerRepository, UserFollowerRepository>();
            builder.Services.AddScoped<IChatRepository, ChatRepository>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddTransient<ISenderEmail, SenderEmail>();
            builder.Services.AddIdentity<User, IdentityRole>(
                options =>
                {
                    // Password settings
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequiredUniqueChars = 4;
                })
                .AddEntityFrameworkStores<Context>()
                .AddDefaultTokenProviders();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =
                JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme =
                JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new
                                SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
            });

            builder.Services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation    
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET 5 Web API",
                    Description = " ITI Projrcy"
                });
                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                    }
                    },
                    new string[] {}
                    }
                    });
            });

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
