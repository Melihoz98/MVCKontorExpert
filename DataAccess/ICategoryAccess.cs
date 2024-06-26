using MVCKontorExpert.Models;

namespace MVCKontorExpert.DataAccess
{
    public interface ICategoryAccess
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int categoryId);
        Task<Category> GetCategoryByName(string categoryName);
        Task<int> AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int categoryId);
    }
}
