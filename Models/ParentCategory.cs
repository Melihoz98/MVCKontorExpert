namespace MVCKontorExpert.Models
{
    public class ParentCategory
    {
        public int ParentCategoryID { get; set; }
        public string ParentCategoryName { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

        public ParentCategory()
        {
            Categories = new List<Category>();
        }

        public ParentCategory(int parentCategoryID, string parentCategoryName)
        {
            ParentCategoryID = parentCategoryID;
            ParentCategoryName = parentCategoryName;
            Categories = new List<Category>();
        }

        // Additional constructors if necessary
    }
}
