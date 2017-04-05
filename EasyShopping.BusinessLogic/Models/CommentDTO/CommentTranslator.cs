using Easyshopping.DataAccess.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public static class CommentTranslator
    {
        public static CommentDTO ToCommentBusiness(this Comment comment)
        {
            if (comment == null) { return null; }

            return new CommentDTO
            {
                Id = comment.ID,
                Description = comment.Description,
                ProductID = comment.ProductID,
                UserID = comment.UserID,
                ParentCmt = comment.ParentCmt,
                CreatedDate = comment.Created_Date,
                ModifiedDate = comment.Modified_Date
            };
        }
        public static IList<CommentDTO> ToCommentBusiness(this IEnumerable<Comment> comments)
        {
            if (comments == null || !comments.Any()) { return null; }

            return comments.Select(e => e.ToCommentBusiness()).ToList();

        }

        public static Comment ToCommentEntity(this CommentDTO comment)
        {
            if (comment == null) { return null; }
            return new Comment
            {
                Id = ,
                Description = comment.Description,
                ProductID = comment.ProductID,
                UserID = comment.UserID,
                ParentCmt = comment.ParentCmt,
                CreatedDate = comment.Created_Date,
                ModifiedDate = comment.Modified_Date
            };
        }
    }
}

