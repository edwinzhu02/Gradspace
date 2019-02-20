using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jupiter.Models
{
    public class ProductCategoryModel
    {
        public int? CategroyId { get; set; }
        public string CategroyName { get; set; }
    }
}