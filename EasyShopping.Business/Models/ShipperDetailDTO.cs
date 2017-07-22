using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public class ShipperDetailDTO
    {
        public int ID { get; set; }
        public Nullable<int> ShipperId { get; set; }
        public Nullable<System.DateTime> RegDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<double> Deposit { get; set; }
        public Nullable<double> Total { get; set; }
        public Nullable<double> RecentBalance { get; set; }
        public string BankAccount { get; set; }
        public Nullable<double> LatX { get; set; }
        public Nullable<double> LatY { get; set; }
    }
}