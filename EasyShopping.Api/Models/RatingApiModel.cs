using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.Api.Models
{
    public class RatingApiModel
    {
        public int ID { get; set; }
        public int Rate { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }

    }
}