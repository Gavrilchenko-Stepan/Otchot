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
                            return new User
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Role = reader["role"].ToString(),
                                FullName = reader["full_name"].ToString(),
                                Login = reader["login"].ToString(),
                                Password = reader["password"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        public User GetGuestUser()
        {
            return new User
            {
                Id = 0,
                Role = "Guest",
                FullName = "Гость",
                Login = "guest",
                Password = ""
            };
        }
    }
}
