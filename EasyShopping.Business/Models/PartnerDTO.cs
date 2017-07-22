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
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedID { get; set; }
        public int UseID { get; set; }
        public string UserName { get; set; }
        public bool isClosed { get; set; }
        public bool isWorking { get; set; }
    }
}