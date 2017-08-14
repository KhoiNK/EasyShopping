using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public class OrderViewDTO
    {
        public int ID { get; set; }
        public string OrderCode { get; set; }
        public string Note { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        //public int UserID { get; set; }
        public DateTime ModifiedDate { get; set; }
        //public int ModifiedID { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }
        public double Total { get; set; }
        //public int CountryID { get; set; }
        public string Country { get; set; }
        //public int CityID { get; set; }
        public string City { get; set; }
        //public int DistrictID { get; set; }
        public string District { get; set; }
        //public int WardID { get; set; }
        public int? StoreId { get; set; }
        public string Store { get; set; }
        public double Price { get; set; }
        public string Shipper { get; set; }
        public int ShipperID { get; set; }
        public int? UserID { get; set; }
        public IEnumerable<OrderDetailDTO> details { get; set; }
        public IEnumerable<OrderViewDTO> Children { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public int ShippingId { get; set; }

    }

    public class OrderDTO
    {
        public int ID { get; set; }
        public string OrderCode { get; set; }
        public string Note { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UserID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedID { get; set; }
        public int StatusID { get; set; }
        public double Total { get; set; }
        public int? CountryID { get; set; }
        public int? CityID { get; set; }
        public int? DistrictID { get; set; }
        public int? WardID { get; set; }
        public int? StoreId { get; set; }
        public double Price { get; set; }
        public int? ParentId { get; set; }
        public Nullable<bool> IsPaid { get; set; }
    }
}