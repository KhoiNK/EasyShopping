using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public class PartnerDTO
    {
        public int ID { get; set; }
        public int StoreID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedID { get; set; }
        public int UserID { get; set; }
    }
}