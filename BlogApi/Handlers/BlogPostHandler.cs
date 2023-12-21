using BlogApi.Data;
using BlogApi.Models;
using BlogApi.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

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

        public static IResult ViewBlogPost(BlogContext context, int id)
        {
            BlogPost? entity =
                context.BlogPosts
                .Where(b => b.Id == id)
                .Include(b => b.Comments)
                .SingleOrDefault();

            if (entity == null)
            {
                return Results.NotFound();
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


    }
}
