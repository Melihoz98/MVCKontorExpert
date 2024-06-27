using MVCKontorExpert.Models;

namespace MVCKontorExpert.BusinessLogic
{
    public interface ICategoryData
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int categoryId);
        Task<Category> GetCategoryByName(string categoryName);
        Task<List<Category>> GetCategoriesByParentCategoryId(int parentCategoryId);
        Task<int> AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int categoryId);
    }
}
