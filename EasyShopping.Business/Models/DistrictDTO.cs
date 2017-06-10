using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public class DistrictDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string LatiLongTude { get; set; }
        public int ProvinceId { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<bool> IsPublished { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}