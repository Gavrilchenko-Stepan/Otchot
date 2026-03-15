using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Article { get; set; }          // article
        public string Name { get; set; }              // name
        public string Unit { get; set; }              // unit
        public decimal Price { get; set; }             // price
        public string Supplier { get; set; }           // supplier
        public string Manufacturer { get; set; }       // manufacturer
        public string Category { get; set; }           // category
        public int CurrentDiscount { get; set; }       // current_discount
        public int StockQuantity { get; set; }         // stock_quantity
        public string Description { get; set; }        // description
        public string Photo { get; set; }              // photo

        public decimal FinalPrice
        {
            get { return Price * (100 - CurrentDiscount) / 100; }
        }

        public bool HasDiscount
        {
            get { return CurrentDiscount > 0; }
        }

        public bool OutOfStock
        {
            get { return StockQuantity <= 0; }
        }

        public bool DiscountMoreThan15
        {
            get { return CurrentDiscount > 15; }
        }
    }
}
