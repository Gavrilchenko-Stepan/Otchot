using MyLib.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public class PickupPointRepository
    {
        private readonly Database _database;

        public PickupPointRepository(Database database)
        {
            _database = database;
        }

        public List<PickupPoint> GetAllPickupPoints()
        {
            List<PickupPoint> points = new List<PickupPoint>();
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                string query = "SELECT id, address FROM pickup_points ORDER BY address";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PickupPoint point = new PickupPoint();
                        point.Id = reader.GetInt32("id");
                        point.Address = reader.GetString("address");
                        points.Add(point);
                    }
                }
            }
            return points;
        }
    }
}
