using Microsoft.AspNetCore.Mvc;
using MVCKontorExpert.BusinessLogic;
using MVCKontorExpert.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

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
            var parentCategories = await _parentCategoryData.GetAllParentCategories();

            foreach (var parentCategory in parentCategories)
            {
                parentCategory.Categories = await _categoryData.GetCategoriesByParentCategoryId(parentCategory.ParentCategoryID);
            }

            var products = await _productData.GetAllProducts();

            var viewModel = new ProductsViewModel
            {
                ParentCategories = parentCategories,
                Products = products
            };

            return View(viewModel);
        }

        public async Task<IActionResult> SortProducts(int sortOrder)
        {
            try
            {
                var products = await _productData.GetAllProducts();

                switch (sortOrder)
                {
                    case 1:
                        products = products.OrderBy(p => p.Price).ToList(); // Lav til Høj
                        break;
                    case 2:
                        products = products.OrderByDescending(p => p.Price).ToList(); // Høj til Lav
                        break;
                    default:
                        break;
                }

                return PartialView("_ProductListPartial", products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sorting products.");
                return BadRequest("Unable to sort products.");
            }
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
