using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.Api.Models
{
    public class ProductApiViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
        public int StoreID { get; set; }
        public string Store { get; set; }
        public string ProductType { get; set; }
        public IEnumerable<ImageApiModel> Images { get; set; }
        public string ThumbailLink { get; set; }
        public string ThumbailCode { get; set; }
        public double Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class ProductApiModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductID { get; set; }
        public int ManufacturedCountryID { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public int StatusID { get; set; }
        public int Quantity { get; set; }
        public int ProductTypeID { get; set; }
        public int StoreID { get; set; }
        public string ThumbailLink { get; set; }
        public string ThumbailCode { get; set; }
        public double Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}