using BlogApi.Data;
using BlogApi.Models;
using BlogApi.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BlogApi.Handlers
{
    public static class CommentHandler
    {
        public static IResult CreateComment(BlogContext context, int id, CommentDto comment)
        {
            BlogPost post = context
                .BlogPosts
                .Where(b => b.Id == id)
                .Include(b => b.Comments)
                .Single();

            post.Comments.Add(new Comment()
            {
                Content = comment.Content,
            });
            context.SaveChanges();
            return Results.StatusCode((int)HttpStatusCode.Created);
        }
    }
}
