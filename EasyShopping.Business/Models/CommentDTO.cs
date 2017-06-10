
using System;

namespace EasyShopping.BusinessLogic.Models
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> ParentCmt { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}