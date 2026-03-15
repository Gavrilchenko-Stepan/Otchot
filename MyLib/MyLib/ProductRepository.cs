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
            List<Product> products = new List<Product>();

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
                            Product product = new Product();
                            product.Article = reader["article"].ToString();
                            product.Name = reader["name"].ToString();
                            product.Unit = reader["unit"].ToString();
                            product.Price = Convert.ToDecimal(reader["price"]);

                            if (reader["supplier"] != DBNull.Value)
                                product.Supplier = reader["supplier"].ToString();
                            else
                                product.Supplier = "—";

                            if (reader["manufacturer"] != DBNull.Value)
                                product.Manufacturer = reader["manufacturer"].ToString();
                            else
                                product.Manufacturer = "—";

                            if (reader["category"] != DBNull.Value)
                                product.Category = reader["category"].ToString();
                            else
                                product.Category = "—";

                            product.CurrentDiscount = Convert.ToInt32(reader["current_discount"]);
                            product.StockQuantity = Convert.ToInt32(reader["stock_quantity"]);

                            if (reader["description"] != DBNull.Value)
                                product.Description = reader["description"].ToString();
                            else
                                product.Description = null;

                            if (reader["photo"] != DBNull.Value)
                                product.Photo = reader["photo"].ToString();
                            else
                                product.Photo = null;

                            products.Add(product);
                        }
                    }
                }
            }
            return products;
        }

        public void AddProduct(Product product)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO products 
                                (article, name, unit, price, supplier, manufacturer, 
                                 category, current_discount, stock_quantity, description, photo)
                                VALUES 
                                (@article, @name, @unit, @price, @supplier, @manufacturer,
                                 @category, @current_discount, @stock_quantity, @description, @photo)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@article", product.Article);
                    cmd.Parameters.AddWithValue("@name", product.Name);
                    cmd.Parameters.AddWithValue("@unit", product.Unit);
                    cmd.Parameters.AddWithValue("@price", product.Price);

                    if (string.IsNullOrEmpty(product.Supplier))
                        cmd.Parameters.AddWithValue("@supplier", "");
                    else
                        cmd.Parameters.AddWithValue("@supplier", product.Supplier);

                    if (string.IsNullOrEmpty(product.Manufacturer))
                        cmd.Parameters.AddWithValue("@manufacturer", "");
                    else
                        cmd.Parameters.AddWithValue("@manufacturer", product.Manufacturer);

                    if (string.IsNullOrEmpty(product.Category))
                        cmd.Parameters.AddWithValue("@category", "");
                    else
                        cmd.Parameters.AddWithValue("@category", product.Category);

                    cmd.Parameters.AddWithValue("@current_discount", product.CurrentDiscount);
                    cmd.Parameters.AddWithValue("@stock_quantity", product.StockQuantity);

                    if (string.IsNullOrEmpty(product.Description))
                        cmd.Parameters.AddWithValue("@description", "");
                    else
                        cmd.Parameters.AddWithValue("@description", product.Description);

                    if (string.IsNullOrEmpty(product.Photo))
                        cmd.Parameters.AddWithValue("@photo", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@photo", product.Photo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                string query = @"UPDATE products SET 
                                name = @name,
                                unit = @unit,
                                price = @price,
                                supplier = @supplier,
                                manufacturer = @manufacturer,
                                category = @category,
                                current_discount = @current_discount,
                                stock_quantity = @stock_quantity,
                                description = @description,
                                photo = @photo
                                WHERE article = @article";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@article", product.Article);
                    cmd.Parameters.AddWithValue("@name", product.Name);
                    cmd.Parameters.AddWithValue("@unit", product.Unit);
                    cmd.Parameters.AddWithValue("@price", product.Price);

                    if (string.IsNullOrEmpty(product.Supplier))
                        cmd.Parameters.AddWithValue("@supplier", "");
                    else
                        cmd.Parameters.AddWithValue("@supplier", product.Supplier);

                    if (string.IsNullOrEmpty(product.Manufacturer))
                        cmd.Parameters.AddWithValue("@manufacturer", "");
                    else
                        cmd.Parameters.AddWithValue("@manufacturer", product.Manufacturer);

                    if (string.IsNullOrEmpty(product.Category))
                        cmd.Parameters.AddWithValue("@category", "");
                    else
                        cmd.Parameters.AddWithValue("@category", product.Category);

                    cmd.Parameters.AddWithValue("@current_discount", product.CurrentDiscount);
                    cmd.Parameters.AddWithValue("@stock_quantity", product.StockQuantity);

                    if (string.IsNullOrEmpty(product.Description))
                        cmd.Parameters.AddWithValue("@description", "");
                    else
                        cmd.Parameters.AddWithValue("@description", product.Description);

                    if (string.IsNullOrEmpty(product.Photo))
                        cmd.Parameters.AddWithValue("@photo", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@photo", product.Photo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProduct(string article)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM products WHERE article = @article";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@article", article);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool IsProductInOrders(string article)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM order_items WHERE product_article = @article";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@article", article);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }
    }
}
