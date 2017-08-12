//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyShopping.Repository.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.ShippingDetails = new HashSet<ShippingDetail>();
        }
    
        public int ID { get; set; }
        public string OrderCode { get; set; }
        public string Note { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<double> Total { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<int> DistrictID { get; set; }
        public Nullable<int> WardID { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<bool> IsTaken { get; set; }
        public Nullable<bool> IsPaid { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual District District { get; set; }
        public virtual Province Province { get; set; }
        public virtual Store Store { get; set; }
        public virtual Ward Ward { get; set; }
        public virtual OrderStatu OrderStatu { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShippingDetail> ShippingDetails { get; set; }
    }
}
