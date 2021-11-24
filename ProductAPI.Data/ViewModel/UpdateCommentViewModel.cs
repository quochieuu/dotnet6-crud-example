namespace ProductAPI.Data.ViewModel
{
    public class UpdateCommentViewModel
    {
        public string Content { get; set; }
        public string Author { get; set; }
        public string Email { get; set; }
        public bool IsAnonymous { get; set; }
        public Guid ProductId { get; set; }
    }
}
