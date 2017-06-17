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
        public string UserName { get; set; }
        public System.DateTime Created_Date { get; set; }
        public System.DateTime Modified_Date { get; set; }
        public string ModifiedUser { get; set; }
        public string Description { get; set; }
        public string Img_Link { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public string Ward { get; set; }
        public int WardId { get; set; }
        public string District { get; set; }
        public int DistrictId { get; set; }
        public string Country { get; set; }
        public int CountryId { get; set; }
        public IEnumerable<ProductApiViewModel> Products{ get; set; }
    }
}