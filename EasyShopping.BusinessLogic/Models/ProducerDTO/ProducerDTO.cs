﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public class ProducerDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public System.Nullable<DateTime> StartDate { get; set; }
        public System.Nullable<DateTime> EndDate { get; set; }
        public int StatusID { get; set; }
        public string Description { get; set; }
    }
}