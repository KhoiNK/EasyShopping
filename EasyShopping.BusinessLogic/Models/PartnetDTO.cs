using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public class PartnerDTO
    {
        public int ID { get; set; }
        public Nullable<int> StoreID { get; set; }
        public System.Nullable<DateTime> CreateDate { get; set; }
        public System.Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedID { get; set; }
        public Nullable<int> UserID { get; set; }
    }
}