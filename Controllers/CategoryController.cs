using kontorExpert.Models;
using MVCKontorExpert.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        public IActionResult Index()
        {
            List<Category> categories = _categoryData.GetAllCategories();
            return View(categories);
        }

        public IActionResult Details(int id)
        {
            Category category = _categoryData.GetCategoryById(id);
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
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryData.AddCategory(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            Category category = _categoryData.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryData.UpdateCategory(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            Category category = _categoryData.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _categoryData.DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
