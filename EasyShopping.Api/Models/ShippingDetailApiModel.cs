using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.Api.Models
{
    public class ShippingDetailApiModel
    {
        public int ID { get; set; }
        public Nullable<int> OrderID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedID { get; set; }
        public Nullable<int> ShipperID { get; set; }
    }

    public class ShippingDetailApiViewModel
    {
        public int ID { get; set; }
        public Nullable<int> OrderID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedID { get; set; }
        public Nullable<int> ShipperID { get; set; }
        public string ShipperName { get; set; }
        public string Status { get; set; }
    }
}