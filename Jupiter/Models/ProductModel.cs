using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;//for validation



namespace Jupiter.Models
{
    public class ProductModel
    {
        public int ProdId { get; set; }
        [Required(ErrorMessage = "Product Type is required")]
        public Nullable<int> ProdTypeId { get; set; }
        [Required(ErrorMessage = "Categroy is required")]
        public Nullable<int> CategroyId { get; set; }
        public string ProdTypeName { get; set; }
        public string CategroyName { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "SubTitle is required")]
        public string SubTitle { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public Nullable<int> TotalStock { get; set; }
        public Nullable<int> AvailableStock { get; set; }
        public Nullable<decimal> Price { get; set; }
        [Range(0, 1, ErrorMessage = "SpcOrDisct must be a number between 0 and 1")]
        public Nullable<short> SpcOrDisct { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> SpecialPrice { get; set; }
        [Range(0, 1, ErrorMessage = "IsActivate must be a number between 0 and 1")]
        public Nullable<byte> IsActivate { get; set; }
        public Nullable<System.DateTime> CreateOn { get; set; }
        public List<string> ProdMedias { get; set; }
    }
}