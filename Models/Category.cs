namespace MVCKontorExpert.Models
{
    public class Category
    {
        public Category()
        {
        }

        public Category(string name) : this()
        {
            CategoryName = name;
        }

        public Category(int id, string name) : this(name)
        {
            CategoryID = id;
        }

        public Category(int id, string name, int? parentCategoryId) : this(id, name)
        {
            ParentCategoryID = parentCategoryId;
        }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategoryID { get; set; }

        // Navigation properties
        public virtual Category ParentCategory { get; set; }
    }
}
