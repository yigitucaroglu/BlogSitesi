namespace BlogProject.ViewModels
{

    public class CommentViewModel
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Content { get; set; }
        public string? UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ParentCommentId { get; set; }

        public List<CommentViewModel> Replies { get; set; } = new();
    }
}
