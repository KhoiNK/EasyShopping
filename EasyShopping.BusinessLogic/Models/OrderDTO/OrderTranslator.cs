using Easyshopping.DataAccess.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public static class OrderTranslator
    {
        public static OrderDTO ToOrderBusiness(this Order order)
        {
            if (order == null) { return null; }

            return new OrderDTO
            {
                ID = order.ID,
                OrderCode = order.Order_Code,
                Note = order.Note,
                Address = order.Address,
                CreateDate = order.CreatedDate,
                UserID = order.UserID,
                ModifiedDate = order.ModifiedDate,
                ModifiedID = order.ModifiedID,
                StatusID = order.StatusID,
                Total = order.Total,
                Taken = order.Taken
            };

        }

        public static IList<OrderDTO> ToOrderBusiness(this IEnumerable<Order> orders)
        {
            if (orders == null || !orders.Any()) { return null; }

            return orders.Select(e => e.ToOrderBusiness()).ToList();

        }

        public static Order ToOrderEntity(this OrderDTO order)
        {
            if (order == null) { return null; }
            return new Order
            {
                ID = order.ID,
                Order_Code = order.OrderCode,
                Note = order.Note,
                Address = order.Address,
                CreatedDate = order.CreateDate,
                UserID = order.UserID,
                ModifiedDate = order.ModifiedDate,
                ModifiedID = order.ModifiedID,
                StatusID = order.StatusID,
                Total = order.Total,
                Taken = order.Taken
            };
        }

        public static IEnumerable<Order> ToOrderEntity(this IList<OrderDTO> orders)
        {
            if (orders == null || !orders.Any()) { return null; }
            return orders.Select(e => e.ToOrderEntity()).ToList();
        }
    }
}
