using Microsoft.AspNetCore.Mvc;
using MVCKontorExpert.BusinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryData _categoryData;

        public CategoryController(ICategoryData categoryData)
        {
            _categoryData = categoryData;
        }

        // Action method to retrieve categories by parent category ID asynchronously
        public async Task<IActionResult> GetCategoriesByParentCategoryId(int parentCategoryId)
        {
            var categories = await _categoryData.GetCategoriesByParentCategoryId(parentCategoryId);
            return PartialView("_SubCategoryDropdown", categories);
        }
    }
}
