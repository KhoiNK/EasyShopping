using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace EasyShopping.Api.Constants
{
    public static class Const
    {
        public static readonly TimeSpan TokenTimeSpan = TimeSpan.FromMinutes(20);
        public static readonly string Issuer = "http://easyshop.local";
        public static readonly byte[] SecretKey;
        public static readonly string Secret;

        static Const()
        {
            // Generate random key
            //RNGCryptoServiceProvider.Create().GetBytes(SecretKey);
            
            // Get base-64 string
            Secret = ConfigurationManager.AppSettings["SecretKey"];
            
            // Decode to byte array. Must be 64 bytes
            SecretKey = TextEncodings.Base64.Decode(Secret);
        }
    }

    public static class Roles
    {
        public const string Admin = "Admin";
    }
}