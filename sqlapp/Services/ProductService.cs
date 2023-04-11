using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;

        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("SQLConnection"));
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            string sqlQuery = "SELECT ProductId, ProductName, Quantity FROM Products";

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(sqlQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product()
                            {
                                Id = reader.GetInt32(0),
                                ProductName = reader.GetString(1),
                                Quantity = reader.GetInt32(2),
                            };

                            products.Add(product);
                        }
                    }
                }
                connection.Close();
                return products;
            }
        }
    }
}
