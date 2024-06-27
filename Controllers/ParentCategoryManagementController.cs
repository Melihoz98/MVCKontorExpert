using Microsoft.AspNetCore.Mvc;
using MVCKontorExpert.BusinessLogic;
using MVCKontorExpert.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCKontorExpert.Controllers
{
    public class ParentCategoryManagementController : Controller
    {
        private readonly IParentCategoryData _parentCategoryData;

        public ParentCategoryManagementController(IParentCategoryData parentCategoryData)
        {
            _parentCategoryData = parentCategoryData;
        }

        public async Task<IActionResult> Index()
        {
            List<ParentCategory> parentCategories = await _parentCategoryData.GetAllParentCategories();
            return View("~/Views/Management/ParentCategoryManagement/Index.cshtml", parentCategories);
        }

        public async Task<IActionResult> Details(int id)
        {
            ParentCategory parentCategory = await _parentCategoryData.GetParentCategoryById(id);
            if (parentCategory == null)
            {
                return NotFound();
            }
            return View(parentCategory);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ParentCategory parentCategory)
        {
            if (ModelState.IsValid)
            {
                await _parentCategoryData.AddParentCategory(parentCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(parentCategory);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ParentCategory parentCategory = await _parentCategoryData.GetParentCategoryById(id);
            if (parentCategory == null)
            {
                return NotFound();
            }
            return View(parentCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ParentCategory parentCategory)
        {
            if (ModelState.IsValid)
            {
                await _parentCategoryData.UpdateParentCategory(parentCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(parentCategory);
        }

        public async Task<IActionResult> Delete(int id)
        {
            ParentCategory parentCategory = await _parentCategoryData.GetParentCategoryById(id);
            if (parentCategory == null)
            {
                return NotFound();
            }
            return View(parentCategory);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _parentCategoryData.DeleteParentCategory(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
