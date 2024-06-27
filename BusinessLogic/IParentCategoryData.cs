using MVCKontorExpert.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCKontorExpert.BusinessLogic
{
    public interface IParentCategoryData
    {
        Task<List<ParentCategory>> GetAllParentCategories();
        Task<ParentCategory> GetParentCategoryById(int parentCategoryId);
        Task<ParentCategory> GetParentCategoryByName(string parentCategoryName);
        Task<int> AddParentCategory(ParentCategory parentCategory);
        Task UpdateParentCategory(ParentCategory parentCategory);
        Task DeleteParentCategory(int parentCategoryId);
    }
}
