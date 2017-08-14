using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.Api.Models
{
    public class ShipperDetailApiModel
    {
        public int ID { get; set; }
        public Nullable<int> ShipperId { get; set; }
        public string Shipper { get; set; }
        public Nullable<System.DateTime> RegDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<double> Deposit { get; set; }
        public Nullable<double> Total { get; set; }
        public Nullable<double> RecentBalance { get; set; }
        public string BankAccount { get; set; }
    }
}