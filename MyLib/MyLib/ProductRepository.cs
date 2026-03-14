using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib.Models;
using MySql.Data.MySqlClient;

namespace MyLib
{
    public class ProductRepository
    {
        private readonly Database _database;

        public ProductRepository(Database database)
        {
            _database = database;
        }

        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();

            string query = @"
                SELECT article, name, unit, price, supplier, manufacturer, 
                       category, current_discount, stock_quantity, description, photo
                FROM products
                ORDER BY name";

            using (var conn = _database.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                Article = reader["article"].ToString(),
                                Name = reader["name"].ToString(),
                                Unit = reader["unit"].ToString(),
                                Price = Convert.ToDecimal(reader["price"]),
                                Supplier = reader["supplier"]?.ToString() ?? "—",
                                Manufacturer = reader["manufacturer"]?.ToString() ?? "—",
                                Category = reader["category"]?.ToString() ?? "—",
                                CurrentDiscount = Convert.ToInt32(reader["current_discount"]),
                                StockQuantity = Convert.ToInt32(reader["stock_quantity"]),
                                Description = reader["description"]?.ToString(),
                                Photo = reader["photo"]?.ToString()
                            });
                        }
                    }
                }
            }

            return products;
        }
    }
}
