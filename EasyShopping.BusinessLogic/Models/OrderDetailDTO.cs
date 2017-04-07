using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public class OrderDetailDTO
    {
        public int ID { get; set; }
        public Nullable<int> OrderID { get; set; }
        public System.Nullable<DateTime> CreateDate { get; set; }
        public Nullable<int> ProductID { get; set; }
        public System.Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedID { get; set; }
        public Nullable<int> Quantity { get; set; }
    }
}