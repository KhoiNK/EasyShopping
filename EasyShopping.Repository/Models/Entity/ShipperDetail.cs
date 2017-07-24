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
    
    public partial class ShipperDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShipperDetail()
        {
            this.ShipperRatings = new HashSet<ShipperRating>();
            this.ShippingDetails = new HashSet<ShippingDetail>();
        }
    
        public int ID { get; set; }
        public Nullable<int> ShipperId { get; set; }
        public Nullable<System.DateTime> RegDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<double> Deposit { get; set; }
        public Nullable<double> Total { get; set; }
        public Nullable<double> RecentBalance { get; set; }
        public string BankAccount { get; set; }
    
        public virtual ShipperStatu ShipperStatu { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShipperRating> ShipperRatings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShippingDetail> ShippingDetails { get; set; }
    }
}
