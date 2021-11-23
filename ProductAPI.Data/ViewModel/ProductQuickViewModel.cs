namespace ProductAPI.Data.ViewModel
{
    public class ProductQuickViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Content { get; set; }
        public string? UrlImage { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ViewCount { get; set; }
        public bool Status { get; set; }
        public Guid CategoryId { get; set; }
    }
}
