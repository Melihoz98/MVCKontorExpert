using Microsoft.AspNetCore.Mvc;
using MVCKontorExpert.BusinessLogic;
using MVCKontorExpert.Models;
using System.Diagnostics;
using System.Collections.Generic;
using kontorExpert.Models;

namespace MVCKontorExpert.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryData _categoryData;

        public HomeController(ILogger<HomeController> logger, ICategoryData categoryData)
        {
            _logger = logger;
            _categoryData = categoryData;
        }

        public IActionResult Index()
        {
            List<Category> categories = _categoryData.GetAllCategories();
            return View(categories);
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
