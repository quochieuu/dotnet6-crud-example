namespace ProductAPI.Data.ViewModel
{
    public class CreateCategoryViewModel
    {
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public bool Status { get; set; }
    }
}
