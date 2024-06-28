using MVCKontorExpert.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCKontorExpert.BusinessLogic
{
    public interface IProductData
    {
        Task<List<Product>> GetAllProducts();
        Task<List<Product>> GetUsedProducts();
        Task<List<Product>> GetNewProducts();
        Task<Product> GetProductById(int productId);
        Task<Product> GetProductByName(string productName);
        Task<int> AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int productId);
        Task<List<Product>> GetProductsByCategoryId(int categoryId);
        Task<List<Product>> GetProductsByParentCategoryId(int parentCategoryId);
    }
}
