using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public class ProductTypeDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class ProductTypeViewDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProductViewDTO> Products { get; set; }
    }
}