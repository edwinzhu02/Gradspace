//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JupiterEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductMedia
    {
        public int Id { get; set; }
        public Nullable<int> ProdId { get; set; }
        public string url { get; set; }
    
        public virtual Product Product { get; set; }
    }
}