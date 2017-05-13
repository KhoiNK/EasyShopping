using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.Api.Models
{
    public class StoreApiModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int UserID { get; set; }
        public System.DateTime Created_Date { get; set; }
        public System.DateTime Modified_Date { get; set; }
        public int Modified_UserID { get; set; }
        public string Description { get; set; }
        public string Img_Link { get; set; }
        public Nullable<int> StatusID { get; set; }
    }
}