namespace ProductAPI.Data.ViewModel
{
    public class UpdateCategoryViewModel
    {
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public bool Status { get; set; }
    }
}
