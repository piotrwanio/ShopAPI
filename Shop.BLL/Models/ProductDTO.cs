using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.BLL.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [MinLength(20)]
        public string Description { get; set; }

        [Required]
        public string Unit { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string PurchasePrice { get; set; }
    }
}
