using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.DAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public string PurchasePrice { get; set; }
    }
}
