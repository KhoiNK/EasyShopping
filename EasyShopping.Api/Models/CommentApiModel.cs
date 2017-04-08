
using System;

namespace EasyShopping.Api.Models
{
    public class CommentApiModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public int ParentCmt { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}