using kontorExpert.Models;
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

        public CategoryController(ICategoryData categoryData)
        {
            _categoryData = categoryData;
        }

        public async Task<IActionResult> Index()
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
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }
    }
}
