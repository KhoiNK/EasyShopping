using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.Api.Models
{
    public class OrderDetailApiModel
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public DateTime CreateDate { get; set; }
        public int ProductID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedID { get; set; }
        public int Quantity { get; set; }
    }
}