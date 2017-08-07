using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public class OrderDetailDTO
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public DateTime CreateDate { get; set; }
        public int ProductID { get; set; }
        public string Product { get; set; }
        public double Price { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedID { get; set; }
        public int Quantity { get; set; }
        public string Img { get; set; }
        public int Weight { get; set; }
    }
}