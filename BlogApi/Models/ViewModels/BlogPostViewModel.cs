namespace BlogApi.Models.ViewModels
{
    public class BlogPostViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public CommentViewModel[] Comments { get; set; }
    }
}
