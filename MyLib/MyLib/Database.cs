using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MyLib
{
    public class Database
    {
        private readonly string connectionString = "Server=localhost;Port=3306;Database=shoe_store;Uid=root;Pwd=vertrigo;";
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
