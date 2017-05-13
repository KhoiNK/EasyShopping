using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public class StoreDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int UserID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public int ModifiedByID { get; set; }
        public string Description { get; set; }
        public string ImgLink { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string BankAccount { get; set; }
        public string TaxCode { get; set; }
    }
}