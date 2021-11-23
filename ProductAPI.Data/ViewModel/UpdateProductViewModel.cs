namespace ProductAPI.Data.ViewModel
{
    public class UpdateProductViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Content { get; set; }
        public string? UrlImage { get; set; }
        public bool Status { get; set; }
        public Guid CategoryId { get; set; }
    }
}
