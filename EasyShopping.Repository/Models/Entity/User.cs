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
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Comments = new HashSet<Comment>();
            this.Orders = new HashSet<Order>();
            this.Partners = new HashSet<Partner>();
            this.Ratings = new HashSet<Rating>();
            this.ShipperDetails = new HashSet<ShipperDetail>();
            this.ShipperRatings = new HashSet<ShipperRating>();
            this.ShippingDetails = new HashSet<ShippingDetail>();
            this.Stores = new HashSet<Store>();
            this.StroreRatings = new HashSet<StroreRating>();
            this.Wishlists = new HashSet<Wishlist>();
        }
    
        public int ID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public System.DateTime DOB { get; set; }
        public string Email { get; set; }
        public System.DateTime RegDate { get; set; }
        public int StatusID { get; set; }
        public string Phone { get; set; }
        public bool Sex { get; set; }
        public int CityID { get; set; }
        public int DistrictID { get; set; }
        public string Address { get; set; }
        public string Img_Link { get; set; }
        public int RoleID { get; set; }
        public System.DateTime Modified_Date { get; set; }
        public int CountryID { get; set; }
        public bool isSocialLogin { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual Country Country { get; set; }
        public virtual District District { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Partner> Partners { get; set; }
        public virtual Province Province { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual Role Role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShipperDetail> ShipperDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShipperRating> ShipperRatings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShippingDetail> ShippingDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Store> Stores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StroreRating> StroreRatings { get; set; }
        public virtual UserStatu UserStatu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}