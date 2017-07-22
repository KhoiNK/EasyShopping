using System;
using System.Collections.Generic;

namespace EasyShopping.BusinessLogic.Models
{
    public class StoreDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedByID { get; set; }
        public string ModifiedUser { get; set; }
        public string Description { get; set; }
        public string ImgLink { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }
        public string BankAccount { get; set; }
        public string TaxCode { get; set; }
        public string Address { get; set; }
        public int WardId { get; set; }
        public string Ward { get; set; }
        public int DistrictId { get; set; }
        public string District { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public double LatX { get; set; }
        public double LatY { get; set; }
        public IEnumerable<ProductDTO> Products { get; set; }
        public bool IsRecruiting { get; set; }
        public string RecruitmentMessage { get; set; }
        public double? RequiredDeposit { get; set; }
    }

}