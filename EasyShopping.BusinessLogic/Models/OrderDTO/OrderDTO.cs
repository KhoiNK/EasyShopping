using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public class OrderDTO
    {
        public int ID { get; set; }
        public string OrderCode { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public System.Nullable<DateTime> CreateDate { get; set; }
        public Nullable <int> UserID { get; set; }
        public System.Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable <int> ModifiedID { get; set; }
        public Nullable <int> StatusID { get; set; }
        public Nullable <double> Total { get; set; }
        public Nullable<bool> Taken { get; set; }
    }
}