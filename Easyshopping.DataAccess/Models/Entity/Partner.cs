//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Easyshopping.DataAccess.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Partner
    {
        public int ID { get; set; }
        public Nullable<int> StoreID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedID { get; set; }
        public Nullable<int> UseID { get; set; }
    
        public virtual Store Store { get; set; }
        public virtual User User { get; set; }
    }
}
