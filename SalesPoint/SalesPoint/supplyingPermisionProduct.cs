//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SalesPoint
{
    using System;
    using System.Collections.Generic;
    
    public partial class supplyingPermisionProduct
    {
        public int permission_id { get; set; }
        public int p_code { get; set; }
        public Nullable<System.DateTime> productuction_date { get; set; }
        public Nullable<int> expire_duration { get; set; }
        public Nullable<int> quantity { get; set; }
    
        public virtual product product { get; set; }
        public virtual supplyingPermission supplyingPermission { get; set; }
    }
}
