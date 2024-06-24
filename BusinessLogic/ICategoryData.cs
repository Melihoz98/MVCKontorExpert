using kontorExpert.Models;
using MVCKontorExpert.Models;

namespace MVCKontorExpert.BusinessLogic
{
    public interface ICategoryData
    {
        List<Category> GetAllCategories();
        Category GetCategoryById(int categoryId);
        Category GetCategoryByName(string categoryName);
        int AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryId);
    }
}
