using Microsoft.FeatureManagement;
using sqlapp.Models;
using System.Data.SqlClient;
using System.Text.Json;

namespace sqlapp.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;

        private readonly IFeatureManager _featureManager;

        public ProductService(IConfiguration configuration, IFeatureManager featureManager)
        {
            _configuration = configuration;
            _featureManager = featureManager;
        }

        public async Task<bool> IsBetaFeatureEnabled()
        {
            return await _featureManager.IsEnabledAsync("betaFeature");
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration["SQLConnection"]);
        }

        /*
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
        */

        public async Task<List<Product>> GetProducts()
        {
            string functionUrl = "https://functionapp1100.azurewebsites.net/api/GetProducts?code=JF8zoZOxbN4m6QbaIudlJQpYBnqW7rkRFget-PHBlhW6AzFuUaOYdA==";

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync(functionUrl);

                string content = await responseMessage.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<Product>>(content);
            }
        }
    }
}
