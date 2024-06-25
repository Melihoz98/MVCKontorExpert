using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MVCKontorExpert.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCKontorExpert.DataAccess
{
    public class ProductAccess : IProductAccess
    {
        private readonly string _connectionString;

        public ProductAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Database connection string is not configured.");
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = new List<Product>();

            try
            {
                const string queryString = "SELECT ProductID, Name, Description, Brand, Price, StockQuantity, Color, Dimensions, CategoryID, IsUsed FROM Products";

                using (var con = new SqlConnection(_connectionString))
                using (var readCommand = new SqlCommand(queryString, con))
                {
                    await con.OpenAsync();

                    using (var productReader = await readCommand.ExecuteReaderAsync())
                    {
                        while (await productReader.ReadAsync())
                        {
                            var product = GetProductFromReader(productReader);
                            products.Add(product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving products: {ex.Message}");
                throw;
            }

            return products;
        }

        public async Task<Product> GetProductById(int productId)
        {
            Product foundProduct = null;

            try
            {
                const string queryString = "SELECT ProductID, Name, Description, Brand, Price, StockQuantity, Color, Dimensions, CategoryID, IsUsed FROM Products WHERE ProductID = @ProductId";

                using (var con = new SqlConnection(_connectionString))
                using (var readCommand = new SqlCommand(queryString, con))
                {
                    readCommand.Parameters.AddWithValue("@ProductId", productId);

                    await con.OpenAsync();

                    using (var productReader = await readCommand.ExecuteReaderAsync())
                    {
                        if (await productReader.ReadAsync())
                        {
                            foundProduct = GetProductFromReader(productReader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving product: {ex.Message}");
                throw;
            }

            return foundProduct;
        }

        public async Task<Product> GetProductByName(string productName)
        {
            Product product = null;
            const string queryString = "SELECT ProductID, Name, Description, Brand, Price, StockQuantity, Color, Dimensions, CategoryID, IsUsed FROM Products WHERE Name = @ProductName";

            using (var con = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(queryString, con))
            {
                command.Parameters.AddWithValue("@ProductName", productName);

                await con.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        product = GetProductFromReader(reader);
                    }
                }
            }

            return product;
        }

        public async Task<int> AddProduct(Product product)
        {
            int insertedId = -1;

            try
            {
                const string insertString = "INSERT INTO Products (Name, Description, Brand, Price, StockQuantity, Color, Dimensions, CategoryID, IsUsed) OUTPUT INSERTED.ProductID VALUES (@Name, @Description, @Brand, @Price, @StockQuantity, @Color, @Dimensions, @CategoryID, @IsUsed)";

                using (var con = new SqlConnection(_connectionString))
                using (var createCommand = new SqlCommand(insertString, con))
                {
                    createCommand.Parameters.AddWithValue("@Name", product.Name);
                    createCommand.Parameters.AddWithValue("@Description", product.Description);
                    createCommand.Parameters.AddWithValue("@Brand", product.Brand);
                    createCommand.Parameters.AddWithValue("@Price", product.Price);
                    createCommand.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);
                    createCommand.Parameters.AddWithValue("@Color", product.Color);
                    createCommand.Parameters.AddWithValue("@Dimensions", product.Dimensions);
                    createCommand.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                    createCommand.Parameters.AddWithValue("@IsUsed", product.IsUsed);

                    await con.OpenAsync();
                    insertedId = (int)await createCommand.ExecuteScalarAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product: {ex.Message}");
                throw;
            }

            return insertedId;
        }

        public async Task UpdateProduct(Product product)
        {
            try
            {
                const string updateString = "UPDATE Products SET Name = @Name, Description = @Description, Brand = @Brand, " +
                                            "Price = @Price, StockQuantity = @StockQuantity, Color = @Color, Dimensions = @Dimensions, " +
                                            "CategoryID = @CategoryID, IsUsed = @IsUsed WHERE ProductID = @ProductId";

                using (var con = new SqlConnection(_connectionString))
                using (var updateCommand = new SqlCommand(updateString, con))
                {
                    updateCommand.Parameters.AddWithValue("@Name", product.Name);
                    updateCommand.Parameters.AddWithValue("@Description", product.Description);
                    updateCommand.Parameters.AddWithValue("@Brand", product.Brand);
                    updateCommand.Parameters.AddWithValue("@Price", product.Price);
                    updateCommand.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);
                    updateCommand.Parameters.AddWithValue("@Color", product.Color);
                    updateCommand.Parameters.AddWithValue("@Dimensions", product.Dimensions);
                    updateCommand.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                    updateCommand.Parameters.AddWithValue("@IsUsed", product.IsUsed);
                    updateCommand.Parameters.AddWithValue("@ProductId", product.ProductID);

                    await con.OpenAsync();
                    await updateCommand.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating product: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteProduct(int productId)
        {
            try
            {
                const string deleteString = "DELETE FROM Products WHERE ProductID = @ProductId";

                using (var con = new SqlConnection(_connectionString))
                using (var deleteCommand = new SqlCommand(deleteString, con))
                {
                    deleteCommand.Parameters.AddWithValue("@ProductId", productId);

                    await con.OpenAsync();
                    await deleteCommand.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting product: {ex.Message}");
                throw;
            }
        }

        private Product GetProductFromReader(SqlDataReader productReader)
        {
            return new Product
            {
                ProductID = productReader.GetInt32(productReader.GetOrdinal("ProductID")),
                Name = productReader.GetString(productReader.GetOrdinal("Name")),
                Description = productReader.GetString(productReader.GetOrdinal("Description")),
                Brand = productReader.GetString(productReader.GetOrdinal("Brand")),
                Price = productReader.GetDecimal(productReader.GetOrdinal("Price")),
                StockQuantity = productReader.GetInt32(productReader.GetOrdinal("StockQuantity")),
                Color = productReader.GetString(productReader.GetOrdinal("Color")),
                Dimensions = productReader.GetString(productReader.GetOrdinal("Dimensions")),
                CategoryID = productReader.GetInt32(productReader.GetOrdinal("CategoryID")),
                IsUsed = productReader.GetBoolean(productReader.GetOrdinal("IsUsed"))
            };
        }
    }
}
