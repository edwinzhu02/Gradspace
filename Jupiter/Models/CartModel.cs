using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Jupiter.Models
{
    public class CartModel
    {
        public int CartID { get; set; }
        public decimal? Price { get; set; }
        [Required (ErrorMessage ="Location is Required.")]
        public string Location { get; set; }
        public DateTime? PlannedTime { get; set; }
        public byte? IsActivate { get; set; }
        public DateTime? CreateOn { get; set; }
        public DateTime? UpdateOn { get; set; }
        public int? ContactId { get; set; }
    }
}