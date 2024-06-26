using MVCKontorExpert.Models;

namespace MVCKontorExpert.DataAccess
{
    public interface IProductAccess
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int productId);
        Task<Product> GetProductByName(string productName);
        Task<int> AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int productId);
    }
}
