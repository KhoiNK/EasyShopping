using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.Api.Models
{
    public class RecruitmentApiModel
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Requirement { get; set; }
        public Nullable<int> StoreId { get; set; }
    }
}