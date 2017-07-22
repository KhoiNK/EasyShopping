using System;

namespace EasyShopping.BusinessLogic.Models
{
    public class MessageDTO
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int SentID { get; set; }
        public int FromID { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRead { get; set; }
    }
}
