using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public class WishlistDTO
    {
        public int ID { get; set; }
        public int ProductDetailID { get; set; }
        public int UserID { get; set; }
    }
}