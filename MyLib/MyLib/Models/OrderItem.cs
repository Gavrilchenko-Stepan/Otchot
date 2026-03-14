using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }               // order_id
        public string ProductArticle { get; set; }     // product_article
        public int Quantity { get; set; }               // quantity
    }
}
