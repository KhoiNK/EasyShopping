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
        public Nullable<int> UserID { get; set; }
        public Nullable<bool> Delay { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> ProvinceID { get; set; }
        public Nullable<int> DistrictID { get; set; }
        public Nullable<int> WardID { get; set; }
        public Nullable<int> ShipperID { get; set; }
    }
}