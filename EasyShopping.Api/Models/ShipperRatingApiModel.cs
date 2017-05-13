using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.Api.Models
{
    public class ShipperRatingApiModel
    {
        public int ID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> ShipperID { get; set; }
        public Nullable<int> Rate { get; set; }
        public string Comment { get; set; }
    }
}