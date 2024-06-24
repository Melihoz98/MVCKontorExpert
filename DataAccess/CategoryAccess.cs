using kontorExpert.Models;
using Microsoft.Data.SqlClient;

namespace MVCKontorExpert.DataAccess
{
    public class CategoryAccess : ICategoryAccess
    {
        private readonly string _connectionString;

        public CategoryAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }
        }

        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();

            try
            {
                string queryString = "SELECT CategoryID, CategoryName FROM Categories";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    con.Open();

                    SqlDataReader categoryReader = readCommand.ExecuteReader();

                    while (categoryReader.Read())
                    {
                        Category category = GetCategoryFromReader(categoryReader);
                        categories.Add(category);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error retrieving categories: {ex.Message}");
                throw;
            }

            return categories;
        }
        public Category GetCategoryById(int categoryId)
        {
            Category foundCategory = null;

            try
            {
                string queryString = "SELECT CategoryID, CategoryName FROM Categories WHERE CategoryID = @CategoryId";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    readCommand.Parameters.AddWithValue("@CategoryId", categoryId);

                    con.Open();

                    using (SqlDataReader categoryReader = readCommand.ExecuteReader())
                    {
                        if (categoryReader.Read())
                        {
                            foundCategory = GetCategoryFromReader(categoryReader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error retrieving category: {ex.Message}");
                throw;
            }

            return foundCategory;
        }
        public Category GetCategoryByName(string categoryName)
        {
            Category category = null;
            string queryString = "SELECT CategoryID, CategoryName FROM Categories WHERE CategoryName = @CategoryName";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(queryString, con))
            {
                command.Parameters.AddWithValue("@CategoryName", categoryName);

                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        category = new Category
                        {
                            CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                            CategoryName = reader.GetString(reader.GetOrdinal("CategoryName"))
                        };
                    }
                }
            }

            return category;
        }
        public int AddCategory(Category category)
        {
            int insertedId = -1;

            try
            {
                string insertString = "INSERT INTO Categories (CategoryName) OUTPUT INSERTED.CategoryID VALUES (@CategoryName)";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand createCommand = new SqlCommand(insertString, con))
                {
                    createCommand.Parameters.AddWithValue("@CategoryName", category.CategoryName);

                    con.Open();
                    insertedId = (int)createCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error adding category: {ex.Message}");
                throw;
            }

            return insertedId;
        }
        public void UpdateCategory(Category category)
        {
            try
            {
                string updateString = "UPDATE Categories SET CategoryName = @CategoryName WHERE CategoryID = @CategoryId";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand updateCommand = new SqlCommand(updateString, con))
                {
                    updateCommand.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    updateCommand.Parameters.AddWithValue("@CategoryId", category.CategoryID);

                    con.Open();
                    updateCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error updating category: {ex.Message}");
                throw;
            }
        }
        public void DeleteCategory(int categoryId)
        {
            try
            {
                string deleteString = "DELETE FROM Categories WHERE CategoryID = @CategoryId";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand deleteCommand = new SqlCommand(deleteString, con))
                {
                    deleteCommand.Parameters.AddWithValue("@CategoryId", categoryId);

                    con.Open();
                    deleteCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting category: {ex.Message}");
                throw;
            }
        }
        private Category GetCategoryFromReader(SqlDataReader categoryReader)
        {
            int categoryId = categoryReader.GetInt32(categoryReader.GetOrdinal("CategoryID"));
            string categoryName = categoryReader.GetString(categoryReader.GetOrdinal("CategoryName"));

            return new Category(categoryId, categoryName);
        }
    }
}
