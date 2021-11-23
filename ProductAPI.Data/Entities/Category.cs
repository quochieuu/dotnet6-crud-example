namespace ProductAPI.Data.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }

    }
}
