using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.BLL.Models
{
    public class CategoryDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Name { get; set; }
    }
}
