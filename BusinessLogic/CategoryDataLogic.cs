using kontorExpert.Models;
using MVCKontorExpert.DataAccess;
using MVCKontorExpert.Models;

namespace MVCKontorExpert.BusinessLogic
{
    public class CategoryDataLogic : ICategoryData
    {
        private readonly ICategoryAccess _categoryAccess;

        public CategoryDataLogic(ICategoryAccess categoryAccess)
        {
            _categoryAccess = categoryAccess;
        }

        public List<Category> GetAllCategories()
        {
            return _categoryAccess.GetAllCategories();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _categoryAccess.GetCategoryById(categoryId);
        }

        public Category GetCategoryByName(string categoryName)
        {
            return _categoryAccess.GetCategoryByName(categoryName);
        }

        public int AddCategory(Category category)
        {
            return _categoryAccess.AddCategory(category);
        }

        public void UpdateCategory(Category category)
        {
            _categoryAccess.UpdateCategory(category);
        }

        public void DeleteCategory(int categoryId)
        {
            _categoryAccess.DeleteCategory(categoryId);
        }
    }
}


