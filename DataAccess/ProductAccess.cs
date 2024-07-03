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
            var products = new Dictionary<int, Product>();

            try
            {
                const string queryString = @"
            SELECT P.ProductID, P.Name, P.Description, P.Brand, P.Price, P.StockQuantity, P.Color, P.Dimensions, P.CategoryID, P.IsUsed,
                   PI.ImageID, PI.ImageUrl
            FROM Products P
            LEFT JOIN ProductImages PI ON P.ProductID = PI.ProductID";

                using (var con = new SqlConnection(_connectionString))
                using (var readCommand = new SqlCommand(queryString, con))
                {
                    await con.OpenAsync();

                    using (var productReader = await readCommand.ExecuteReaderAsync())
                    {
                        while (await productReader.ReadAsync())
                        {
                            int productId = productReader.GetInt32(productReader.GetOrdinal("ProductID"));

                            if (!products.ContainsKey(productId))
                            {
                                var product = new Product
                                {
                                    ProductID = productId,
                                    Name = productReader.GetString(productReader.GetOrdinal("Name")),
                                    Description = productReader.GetString(productReader.GetOrdinal("Description")),
                                    Brand = productReader.GetString(productReader.GetOrdinal("Brand")),
                                    Price = productReader.GetDecimal(productReader.GetOrdinal("Price")),
                                    StockQuantity = productReader.GetInt32(productReader.GetOrdinal("StockQuantity")),
                                    Color = productReader.GetString(productReader.GetOrdinal("Color")),
                                    Dimensions = productReader.GetString(productReader.GetOrdinal("Dimensions")),
                                    CategoryID = productReader.GetInt32(productReader.GetOrdinal("CategoryID")),
                                    IsUsed = productReader.GetBoolean(productReader.GetOrdinal("IsUsed")),
                                    Images = new List<ProductImage>()
                                };
                                products[productId] = product;
                            }

                            if (!productReader.IsDBNull(productReader.GetOrdinal("ImageID")))
                            {
                                var image = new ProductImage
                                {
                                    ImageID = productReader.GetInt32(productReader.GetOrdinal("ImageID")),
                                    ImageUrl = productReader.GetString(productReader.GetOrdinal("ImageUrl"))
                                };
                                products[productId].Images.Add(image);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving products: {ex.Message}");
                throw;
            }

            return new List<Product>(products.Values);
        }

        public async Task<List<Product>> GetProductsByCategoryID(int categoryID)
        {
            var products = new Dictionary<int, Product>();

            try
            {
                const string queryString = @"
            SELECT P.ProductID, P.Name, P.Description, P.Brand, P.Price, P.StockQuantity, P.Color, P.Dimensions, P.CategoryID, P.IsUsed,
                   PI.ImageID, PI.ImageUrl
            FROM Products P
            LEFT JOIN ProductImages PI ON P.ProductID = PI.ProductID
            WHERE P.CategoryID = @CategoryID";

                using (var con = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(queryString, con))
                {
                    command.Parameters.AddWithValue("@CategoryID", categoryID);

                    await con.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int productId = reader.GetInt32(reader.GetOrdinal("ProductID"));

                            if (!products.ContainsKey(productId))
                            {
                                var product = new Product
                                {
                                    ProductID = productId,
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Description = reader.GetString(reader.GetOrdinal("Description")),
                                    Brand = reader.GetString(reader.GetOrdinal("Brand")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                    StockQuantity = reader.GetInt32(reader.GetOrdinal("StockQuantity")),
                                    Color = reader.GetString(reader.GetOrdinal("Color")),
                                    Dimensions = reader.GetString(reader.GetOrdinal("Dimensions")),
                                    CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                    IsUsed = reader.GetBoolean(reader.GetOrdinal("IsUsed")),
                                    Images = new List<ProductImage>()
                                };
                                products[productId] = product;
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("ImageID")))
                            {
                                var image = new ProductImage
                                {
                                    ImageID = reader.GetInt32(reader.GetOrdinal("ImageID")),
                                    ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl"))
                                };
                                products[productId].Images.Add(image);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving products by category ID: {ex.Message}");
                throw;
            }

            return new List<Product>(products.Values);
        }

        public async Task<List<Product>> GetUsedProducts()
        {
            var usedProducts = new List<Product>();

            try
            {
                const string queryString = "SELECT ProductID, Name, Description, Brand, Price, StockQuantity, Color, Dimensions, CategoryID, IsUsed FROM Products WHERE IsUsed = 1";

                using (var con = new SqlConnection(_connectionString))
                using (var readCommand = new SqlCommand(queryString, con))
                {
                    await con.OpenAsync();

                    using (var productReader = await readCommand.ExecuteReaderAsync())
                    {
                        while (await productReader.ReadAsync())
                        {
                            var product = GetProductFromReader(productReader);
                            usedProducts.Add(product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving used products: {ex.Message}");
                throw;
            }

            return usedProducts;
        }

        public async Task<List<Product>> GetNewProducts()
        {
            var newProducts = new List<Product>();

            try
            {
                const string queryString = "SELECT ProductID, Name, Description, Brand, Price, StockQuantity, Color, Dimensions, CategoryID, IsUsed FROM Products WHERE IsUsed = 0";

                using (var con = new SqlConnection(_connectionString))
                using (var readCommand = new SqlCommand(queryString, con))
                {
                    await con.OpenAsync();

                    using (var productReader = await readCommand.ExecuteReaderAsync())
                    {
                        while (await productReader.ReadAsync())
                        {
                            var product = GetProductFromReader(productReader);
                            newProducts.Add(product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving new products: {ex.Message}");
                throw;
            }

            return newProducts;
        }

        public async Task<Product> GetProductById(int productId)
        {
            Product foundProduct = null;

            try
            {
                const string queryString = @"
        SELECT P.ProductID, P.Name, P.Description, P.Brand, P.Price, P.StockQuantity, P.Color, P.Dimensions, P.CategoryID, P.IsUsed,
               PI.ImageID, PI.ImageUrl
        FROM Products P
        LEFT JOIN ProductImages PI ON P.ProductID = PI.ProductID
        WHERE P.ProductID = @ProductId";

                using (var con = new SqlConnection(_connectionString))
                using (var readCommand = new SqlCommand(queryString, con))
                {
                    readCommand.Parameters.AddWithValue("@ProductId", productId);

                    await con.OpenAsync();

                    using (var productReader = await readCommand.ExecuteReaderAsync())
                    {
                        while (await productReader.ReadAsync())
                        {
                            if (foundProduct == null)
                            {
                                foundProduct = new Product
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
                                    IsUsed = productReader.GetBoolean(productReader.GetOrdinal("IsUsed")),
                                    Images = new List<ProductImage>()
                                };
                            }

                            if (!productReader.IsDBNull(productReader.GetOrdinal("ImageID")))
                            {
                                foundProduct.Images.Add(new ProductImage
                                {
                                    ImageID = productReader.GetInt32(productReader.GetOrdinal("ImageID")),
                                    ImageUrl = productReader.GetString(productReader.GetOrdinal("ImageUrl"))
                                });
                            }
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

            try
            {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving product: {ex.Message}");
                throw;
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
