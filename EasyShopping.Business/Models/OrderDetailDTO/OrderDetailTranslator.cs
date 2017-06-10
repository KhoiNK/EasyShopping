using Easyshopping.DataAccess.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public static class OrderDetailTranslator
    {
        public static OrderDetailDTO ToOrderDetailBusiness(this OrderDetail orderdetail)
        {
            if (orderdetail == null) { return null; }

            return new OrderDetailDTO
            {
                ID = orderdetail.ID,
                OrderID = orderdetail.OrderID,
                CreateDate = orderdetail.CreatedDate,
                ProductID = orderdetail.ProductID,
                ModifiedDate = orderdetail.ModifiedDate,
                ModifiedID = orderdetail.ModifiedID,
                Quantity = orderdetail.Quantity
            };

        }

        public static IList<OrderDetailDTO> ToOrderDetailBusiness(this IEnumerable<OrderDetail> orderdetails)
        {
            if (orderdetails == null || !orderdetails.Any()) { return null; }

            return orderdetails.Select(e => e.ToOrderDetailBusiness()).ToList();

        }

        public static OrderDetail ToOrderDetailEntity(this OrderDetailDTO orderdetail)
        {
            if (orderdetail == null) { return null; }
            return new OrderDetail
            {
                ID = orderdetail.ID,
                OrderID = orderdetail.OrderID,
                CreateDate = orderdetail.CreateDate,
                ProductID = orderdetail.ProductID,
                ModifiedDate = orderdetail.ModifiedDate,
                ModifiedID = orderdetail.ModifiedID,
                Quantity = orderdetail.Quantity
            };
        }

        public static IEnumerable<OrderDetail> ToOrderDetailEntity(this IList<OrderDetailDTO> orderdetails)
        {
            if (orderdetails == null || !orderdetails.Any()) { return null; }
            return orderdetails.Select(e => e.ToOrderDetailEntity()).ToList();
        }
    }
}

