using MVCKontorExpert.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCKontorExpert.Models;

namespace MVCKontorExpert.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryData _categoryData;
        private readonly IParentCategoryData _parentCategoryData;

        public CategoryController(ICategoryData categoryData, IParentCategoryData parentCategoryData)
        {
            _categoryData = categoryData;
            _parentCategoryData = parentCategoryData;
        }

        public async Task<IActionResult> CategoryManagement()
        {
            List<Category> categories = await _categoryData.GetAllCategories();
            return View(categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            Category category = await _categoryData.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryData.AddCategory(category);
                return RedirectToAction(nameof(CategoryManagement));
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Category category = await _categoryData.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryData.UpdateCategory(category);
                return RedirectToAction(nameof(CategoryManagement));
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Category category = await _categoryData.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryData.DeleteCategory(id);
            return RedirectToAction(nameof(CategoryManagement));
        }

        public async Task<IActionResult> GetSubCategories(int parentId)
        {
            var subCategories = await _categoryData.GetCategoriesByParentCategoryId(parentId);
            return Json(subCategories);
        }

    }
}
