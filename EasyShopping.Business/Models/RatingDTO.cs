﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public class RatingDTO
    {
        public int ID { get; set; }
        public int Rate { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
    }
}