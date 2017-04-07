
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> ParentCmt { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}