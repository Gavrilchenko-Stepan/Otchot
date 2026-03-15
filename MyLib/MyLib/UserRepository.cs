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
    public class UserRepository
    {
        private readonly Database _database;

        public UserRepository(Database database)
        {
            _database = database;
        }

        public User Authenticate(string login, string password)
        {
            string query = @"
                SELECT id, role, full_name, login, password
                FROM users
                WHERE login = @login AND password = @password";

            using (var conn = _database.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User user = new User();
                            user.Id = Convert.ToInt32(reader["id"]);
                            user.Role = reader["role"].ToString();
                            user.FullName = reader["full_name"].ToString();
                            user.Login = reader["login"].ToString();
                            user.Password = reader["password"].ToString();
                            return user;
                        }
                    }
                }
            }
            return null;
        }

        public User GetGuestUser()
        {
            User guest = new User();
            guest.Id = 0;
            guest.Role = "Гость";
            guest.FullName = "Гость";
            guest.Login = "guest";
            guest.Password = "";
            return guest;
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            
            string query = @"SELECT id, role, full_name, login, password
                            FROM users";
            
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User();
                            user.Id = Convert.ToInt32(reader["id"]);
                            user.Role = reader["role"].ToString();
                            user.FullName = reader["full_name"].ToString();
                            user.Login = reader["login"].ToString();
                            user.Password = reader["password"].ToString();
                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }

        public List<User> GetUsersByRole(string role)
        {
            List<User> users = new List<User>();
            
            string query = @"SELECT id, role, full_name, login, password
                            FROM users
                            WHERE role = @role";
            
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@role", role);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User();
                            user.Id = Convert.ToInt32(reader["id"]);
                            user.Role = reader["role"].ToString();
                            user.FullName = reader["full_name"].ToString();
                            user.Login = reader["login"].ToString();
                            user.Password = reader["password"].ToString();
                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }
    }
}
