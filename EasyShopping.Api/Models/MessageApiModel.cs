using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.Api.Models
{
    public class MessageApiModel
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int SentID { get; set; }
        public string Sent { get; set; }
        public int FromID { get; set; }
        public string From { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRead { get; set; }
    }
}