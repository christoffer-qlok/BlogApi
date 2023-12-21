using BlogApi.Data;
using BlogApi.Models;
using BlogApi.Models.DTO;
using BlogApi.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BlogApi.Handlers
{
    public static class BlogPostHandler
    {
        public static IResult ListBlogPosts(BlogContext context)
        {
            BlogPostListViewModel[] result =
                context.BlogPosts
                .Select(b => new BlogPostListViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                }).ToArray();
            return Results.Json(result);
        }

        public static IResult ViewSingleBlogPost(BlogContext context, int id)
        {
            BlogPost? entity =
                context.BlogPosts
                .Where(b => b.Id == id)
                .Include(b => b.Comments)
                .SingleOrDefault();

            if (entity == null)
            {
                return Results.NotFound(new { Message = "No such blog post"});
            }

            BlogPostViewModel result = new BlogPostViewModel()
            {
                Title = entity.Title,
                Content = entity.Content,
                Comments = entity.Comments
                .Select(c => new CommentViewModel()
                {
                    Content = c.Content,
                })
                .ToArray()
            };

            return Results.Json(result);
        }

        public static IResult CreateBlogPost(BlogContext context, BlogPostDto blogPost) 
        {
            if(string.IsNullOrEmpty(blogPost.Content) || string.IsNullOrEmpty(blogPost.Title))
            {
                return Results.BadRequest(new { Message = "Blog posts needs to have both title and content" });
            }
            context.BlogPosts.Add(new BlogPost()
            {
                Title = blogPost.Title,
                Content = blogPost.Content,
            });
            context.SaveChanges();
            return Results.StatusCode((int)HttpStatusCode.Created);
        }
    }
}
