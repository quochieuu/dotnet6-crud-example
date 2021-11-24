namespace ProductAPI.Data.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Email { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
