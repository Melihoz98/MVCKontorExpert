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
       

        public HomeController(ILogger<HomeController> logger, ICategoryData categoryData, IParentCategoryData parentCategoryData)
        {
            _logger = logger;
            _categoryData = categoryData;
            _parentCategoryData = parentCategoryData;
           
        }

        public async Task<IActionResult> Index()
        {
            var parentCategories = await _parentCategoryData.GetAllParentCategories();

            // Assuming GetCategoriesByParentCategoryId returns categories for each parent category
            foreach (var parentCategory in parentCategories)
            {
                parentCategory.Categories = await _categoryData.GetCategoriesByParentCategoryId(parentCategory.ParentCategoryID);
            }

            var viewModel = new ProductsViewModel
            {
                ParentCategories = parentCategories
            };

            return View(viewModel);
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
