using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }           // order_number
        public DateTime OrderDate { get; set; }        // order_date
        public DateTime? DeliveryDate { get; set; }    // delivery_date
        public int? PickupPointId { get; set; }        // pickup_point_id
        public int? CustomerId { get; set; }           // customer_id
        public string PickupCode { get; set; }         // pickup_code
        public string Status { get; set; }             // status
    }
}
