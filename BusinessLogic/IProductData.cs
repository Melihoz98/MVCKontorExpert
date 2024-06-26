using MVCKontorExpert.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCKontorExpert.BusinessLogic
{
    public interface IProductData
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int productId);
        Task<Product> GetProductByName(string productName);
        Task<int> AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int productId);
    }
}
