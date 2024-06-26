using Microsoft.AspNetCore.Mvc;
using MVCKontorExpert.BusinessLogic;
using MVCKontorExpert.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCKontorExpert.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductData _productData;

        public ProductController(IProductData productData)
        {
            _productData = productData;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products = await _productData.GetAllProducts();
            return View("ProductManagement", products);
        }

        public async Task<IActionResult> Details(int id)
        {
            Product product = await _productData.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                // Add product logic
                await _productData.AddProduct(product);
                return RedirectToAction(nameof(Index));
            }

            // If ModelState is not valid, debug to check validation errors
            foreach (var modelStateEntry in ModelState.Values)
            {
                foreach (var error in modelStateEntry.Errors)
                {
                    // Debug or log the validation errors
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            // Return the view with the invalid product model
            return View(product);
        }


        public async Task<IActionResult> Edit(int id)
        {
            Product product = await _productData.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                await _productData.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _productData.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productData.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
