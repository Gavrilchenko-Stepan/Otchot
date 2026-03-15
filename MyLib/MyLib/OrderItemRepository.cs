using MyLib.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public class OrderItemRepository
    {
        private readonly Database _database;

        public OrderItemRepository(Database database)
        {
            _database = database;
        }

        public List<OrderItem> GetItemsByOrderId(int orderId)
        {
            List<OrderItem> items = new List<OrderItem>();
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                string query = "SELECT id, order_id, product_article, quantity FROM order_items WHERE order_id = @orderId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderItem item = new OrderItem();
                            item.Id = Convert.ToInt32(reader["id"]);
                            item.OrderId = Convert.ToInt32(reader["order_id"]);
                            item.ProductArticle = reader["product_article"].ToString();
                            item.Quantity = Convert.ToInt32(reader["quantity"]);
                            items.Add(item);
                        }
                    }
                }
            }
            return items;
        }

        public void AddOrderItem(OrderItem item)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO order_items (order_id, product_article, quantity) VALUES (@order_id, @product_article, @quantity)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@order_id", item.OrderId);
                    cmd.Parameters.AddWithValue("@product_article", item.ProductArticle);
                    cmd.Parameters.AddWithValue("@quantity", item.Quantity);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteOrderItemsByOrderId(int orderId)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM order_items WHERE order_id = @orderId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
