using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.Api.Models
{
    public class ProductTypeApiModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> CateID { get; set; }
    }
}