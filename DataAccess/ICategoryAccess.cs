using kontorExpert.Models;

namespace MVCKontorExpert.DataAccess
{
    public interface ICategoryAccess
    {
        List<Category> GetAllCategories();
        Category GetCategoryById(int categoryId);
        Category GetCategoryByName(string categoryName);
        int AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryId);
    }
}
