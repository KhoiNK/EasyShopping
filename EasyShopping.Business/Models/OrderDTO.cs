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
        public DateTime CreateDate { get; set; }
        public int UserID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedID { get; set; }
        public int StatusID { get; set; }
        public double Total { get; set; }
        public bool Taken { get; set; }
    }
}