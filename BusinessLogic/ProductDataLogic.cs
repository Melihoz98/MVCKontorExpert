using MVCKontorExpert.DataAccess;
using MVCKontorExpert.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCKontorExpert.BusinessLogic
{
    public class ProductDataLogic : IProductData
    {
        private readonly IProductAccess _productAccess;

        public ProductDataLogic(IProductAccess productAccess)
        {
            _productAccess = productAccess;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _productAccess.GetAllProducts();
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _productAccess.GetProductById(productId);
        }

        public async Task<Product> GetProductByName(string productName)
        {
            return await _productAccess.GetProductByName(productName);
        }

        public async Task<int> AddProduct(Product product)
        {
            return await _productAccess.AddProduct(product);
        }

        public async Task UpdateProduct(Product product)
        {
            await _productAccess.UpdateProduct(product);
        }

        public async Task DeleteProduct(int productId)
        {
            await _productAccess.DeleteProduct(productId);
        }
    }
}
