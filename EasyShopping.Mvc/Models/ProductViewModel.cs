using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.Mvc.Models
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductID { get; set; }
        public int Manufactured_CountryID { get; set; }
        public int ProducerID { get; set; }
        public double Weight { get; set; }
        public int StatusID { get; set; }
        public int Quantity { get; set; }
        public int ProductTypeID { get; set; }
        public int StoreID { get; set; }
        public IList<ImageViewModel> Images { get; set; }
    }
}