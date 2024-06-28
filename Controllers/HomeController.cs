using Microsoft.AspNetCore.Mvc;
using MVCKontorExpert.BusinessLogic;
using MVCKontorExpert.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MVCKontorExpert.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryData _categoryData;
        private readonly IParentCategoryData _parentCategoryData;
        private readonly IProductData _productData;

        public HomeController(ILogger<HomeController> logger, ICategoryData categoryData, IParentCategoryData parentCategoryData, IProductData productData)
        {
            _logger = logger;
            _categoryData = categoryData;
            _parentCategoryData = parentCategoryData;
            _productData = productData;
        }

        public async Task<IActionResult> Index()
        {
            // Retrieve all parent categories
            var parentCategories = await _parentCategoryData.GetAllParentCategories();

            // Loop through each parent category to fetch associated categories
            foreach (var parentCategory in parentCategories)
            {
                // Fetch categories for the current parent category
                parentCategory.Categories = await _categoryData.GetCategoriesByParentCategoryId(parentCategory.ParentCategoryID);
            }

            // Pass the model to the view
            return View(parentCategories);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
