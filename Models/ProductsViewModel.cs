namespace MVCKontorExpert.Models
{
    public class ProductsViewModel
    {
        public List<ParentCategory> ParentCategories { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
    }
}
