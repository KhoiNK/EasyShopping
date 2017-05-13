using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.Api.Models
{
    public class StoreRatingApiModel
    {
        public int ID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> StoreID { get; set; }
        public Nullable<int> Rate { get; set; }
        public string Comment { get; set; }
    }
}