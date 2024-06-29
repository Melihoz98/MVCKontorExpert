using Microsoft.AspNetCore.Mvc;
using MVCKontorExpert.BusinessLogic;
using MVCKontorExpert.Models;

namespace MVCKontorExpert.Controllers
{


    public class ProductController : Controller
    {
        private readonly IProductData _productData;

        public ProductController(IProductData productData)
        {
            _productData = productData;
        }

        public async Task<IActionResult> Index(int categoryID)
        {
            var products = await _productData.GetProductsByCategoryID(categoryID);

            var viewModel = new ProductsViewModel
            {
                Products = products
            };

            return View(viewModel);
        }

    }
}