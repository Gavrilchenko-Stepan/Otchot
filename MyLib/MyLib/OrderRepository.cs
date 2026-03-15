using MyLib.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public class OrderRepository
    {
        private readonly Database _database;

        public OrderRepository(Database database)
        {
            _database = database;
        }

        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();

            using (var conn = _database.GetConnection())
            {
                conn.Open();
                string query = @"SELECT id, order_number, order_date, delivery_date, 
                                        pickup_point_id, customer_id, pickup_code, status
                                 FROM orders ORDER BY order_date DESC";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order();
                        order.Id = Convert.ToInt32(reader["id"]);
                        order.OrderNumber = Convert.ToInt32(reader["order_number"]);
                        order.OrderDate = Convert.ToDateTime(reader["order_date"]);

                        if (reader["delivery_date"] != DBNull.Value)
                            order.DeliveryDate = Convert.ToDateTime(reader["delivery_date"]);
                        else
                            order.DeliveryDate = null;

                        if (reader["pickup_point_id"] != DBNull.Value)
                            order.PickupPointId = Convert.ToInt32(reader["pickup_point_id"]);
                        else
                            order.PickupPointId = null;

                        if (reader["customer_id"] != DBNull.Value)
                            order.CustomerId = Convert.ToInt32(reader["customer_id"]);
                        else
                            order.CustomerId = null;

                        order.PickupCode = reader["pickup_code"].ToString();
                        order.Status = reader["status"].ToString();

                        orders.Add(order);
                    }
                }
            }
            return orders;
        }

        public Order GetOrderById(int id)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                string query = @"SELECT id, order_number, order_date, delivery_date, 
                                        pickup_point_id, customer_id, pickup_code, status
                                 FROM orders WHERE id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Order order = new Order();
                            order.Id = Convert.ToInt32(reader["id"]);
                            order.OrderNumber = Convert.ToInt32(reader["order_number"]);
                            order.OrderDate = Convert.ToDateTime(reader["order_date"]);

                            if (reader["delivery_date"] != DBNull.Value)
                                order.DeliveryDate = Convert.ToDateTime(reader["delivery_date"]);
                            else
                                order.DeliveryDate = null;

                            if (reader["pickup_point_id"] != DBNull.Value)
                                order.PickupPointId = Convert.ToInt32(reader["pickup_point_id"]);
                            else
                                order.PickupPointId = null;

                            if (reader["customer_id"] != DBNull.Value)
                                order.CustomerId = Convert.ToInt32(reader["customer_id"]);
                            else
                                order.CustomerId = null;

                            order.PickupCode = reader["pickup_code"].ToString();
                            order.Status = reader["status"].ToString();

                            return order;
                        }
                    }
                }
            }
            return null;
        }

        public void AddOrder(Order order)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO orders 
                                (order_number, order_date, delivery_date, pickup_point_id, customer_id, pickup_code, status)
                                VALUES 
                                (@order_number, @order_date, @delivery_date, @pickup_point_id, @customer_id, @pickup_code, @status);
                                SELECT LAST_INSERT_ID();";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@order_number", order.OrderNumber);
                    cmd.Parameters.AddWithValue("@order_date", order.OrderDate);

                    if (order.DeliveryDate.HasValue)
                        cmd.Parameters.AddWithValue("@delivery_date", order.DeliveryDate.Value);
                    else
                        cmd.Parameters.AddWithValue("@delivery_date", DBNull.Value);

                    if (order.PickupPointId.HasValue)
                        cmd.Parameters.AddWithValue("@pickup_point_id", order.PickupPointId.Value);
                    else
                        cmd.Parameters.AddWithValue("@pickup_point_id", DBNull.Value);

                    if (order.CustomerId.HasValue)
                        cmd.Parameters.AddWithValue("@customer_id", order.CustomerId.Value);
                    else
                        cmd.Parameters.AddWithValue("@customer_id", DBNull.Value);

                    cmd.Parameters.AddWithValue("@pickup_code", order.PickupCode);
                    cmd.Parameters.AddWithValue("@status", order.Status);

                    order.Id = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public void UpdateOrder(Order order)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                string query = @"UPDATE orders SET 
                                order_number = @order_number, 
                                order_date = @order_date, 
                                delivery_date = @delivery_date, 
                                pickup_point_id = @pickup_point_id, 
                                customer_id = @customer_id, 
                                pickup_code = @pickup_code, 
                                status = @status
                                WHERE id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", order.Id);
                    cmd.Parameters.AddWithValue("@order_number", order.OrderNumber);
                    cmd.Parameters.AddWithValue("@order_date", order.OrderDate);

                    if (order.DeliveryDate.HasValue)
                        cmd.Parameters.AddWithValue("@delivery_date", order.DeliveryDate.Value);
                    else
                        cmd.Parameters.AddWithValue("@delivery_date", DBNull.Value);

                    if (order.PickupPointId.HasValue)
                        cmd.Parameters.AddWithValue("@pickup_point_id", order.PickupPointId.Value);
                    else
                        cmd.Parameters.AddWithValue("@pickup_point_id", DBNull.Value);

                    if (order.CustomerId.HasValue)
                        cmd.Parameters.AddWithValue("@customer_id", order.CustomerId.Value);
                    else
                        cmd.Parameters.AddWithValue("@customer_id", DBNull.Value);

                    cmd.Parameters.AddWithValue("@pickup_code", order.PickupCode);
                    cmd.Parameters.AddWithValue("@status", order.Status);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteOrder(int id)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM orders WHERE id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
