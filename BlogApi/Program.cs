using BlogApi.Data;
using BlogApi.Handlers;
using Microsoft.EntityFrameworkCore;

namespace BlogApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("BlogContext");
            builder.Services.AddDbContext<BlogContext>(opt => opt.UseSqlServer(connectionString));
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            // Blog posts
            app.MapGet("/blog", BlogPostHandler.ListBlogPosts);
            app.MapGet("/blog/{id}", BlogPostHandler.ViewSingleBlogPost);
            app.MapPost("/blog", BlogPostHandler.CreateBlogPost);

            // Comments
            app.MapPost("/blog/{id}/comment", CommentHandler.CreateComment);

            app.Run();
        }
    }
}
