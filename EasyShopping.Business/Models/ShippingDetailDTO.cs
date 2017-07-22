using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public class ShippingDetailDTO
    {
        public int ID { get; set; }
        public Nullable<int> OrderID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedID { get; set; }
        public Nullable<int> ShipperID { get; set; }
    }

    public class ShippingDetailViewDTO
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