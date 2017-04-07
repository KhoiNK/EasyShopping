using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.Api.Constants
{
    public static class Const
    {
        public static readonly TimeSpan TokenTimeSpan = TimeSpan.FromMinutes(20);
        public static readonly string Issuer = "http://easyshop.local";
        public static readonly string Secret = "bmd1eWVua2lta2hvaQ==";
    }
}