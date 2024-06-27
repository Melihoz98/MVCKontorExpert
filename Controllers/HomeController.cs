using Microsoft.AspNetCore.Mvc;
using MVCKontorExpert.BusinessLogic;
using MVCKontorExpert.Models;
using System.Diagnostics;
using System.Threading.Tasks;

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
            var categories = await _categoryData.GetAllCategories();
            var parentCategories = await _parentCategoryData.GetAllParentCategories();
            var usedProducts = await _productData.GetUsedProducts();
            var newProducts = await _productData.GetNewProducts(); 
            var hjemmekontor = await _categoryData.GetCategoriesByParentCategoryId(8);
            var akustik = await _categoryData.GetCategoriesByParentCategoryId(6);
            var diverse = await _categoryData.GetCategoriesByParentCategoryId(7);

            ViewBag.Categories = categories;
            ViewBag.ParentCategories = parentCategories;
            ViewBag.UsedProducts = usedProducts;
            ViewBag.NewProducts = newProducts;
            ViewBag.Hjemmekontor = hjemmekontor;
            ViewBag.Akustik = akustik;
            ViewBag.Diverse = diverse;

            return View();
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
