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
    
    public partial class Store
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Store()
        {
            this.Orders = new HashSet<Order>();
            this.Partners = new HashSet<Partner>();
            this.Products = new HashSet<Product>();
            this.StoreRatings = new HashSet<StoreRating>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public int UserID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public int ModifiedByID { get; set; }
        public string Description { get; set; }
        public string ImgLink { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string BankAccount { get; set; }
        public string TaxCode { get; set; }
        public string Address { get; set; }
        public Nullable<int> WardId { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> CityId { get; set; }
        public Nullable<double> LatX { get; set; }
        public Nullable<double> LatY { get; set; }
        public Nullable<double> RequiredDeposit { get; set; }
        public Nullable<bool> IsRecruiting { get; set; }
        public string RecruitmentMessage { get; set; }
        public Nullable<int> LimitProduct { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual District District { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Partner> Partners { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
        public virtual Province Province { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreRating> StoreRatings { get; set; }
        public virtual Ward Ward { get; set; }
        public virtual StoreStatu StoreStatu { get; set; }
        public virtual User User { get; set; }
    }
}
