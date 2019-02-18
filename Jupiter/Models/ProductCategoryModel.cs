using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jupiter.Models
{
    public class ProductCategoryModel
    {
        [Required(ErrorMessage = "Categoty Id is required.")]
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}