using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.Api.Models
{
    public class OrderApiModel
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
        public int ParentId { get; set; }
    }

    public class AddToCartModel
    {
        public int productId {get; set;}
        public int cartId { get; set; }
    }
}