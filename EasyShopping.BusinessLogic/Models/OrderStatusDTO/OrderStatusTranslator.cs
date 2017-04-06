using Easyshopping.DataAccess.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public static class OrderStatusTranslator
    {
        public static OrderStatusDTO ToOrderStatusBusiness(this OrderStatu orderstatus)
        {
            if (orderstatus == null) { return null; }

            return new OrderStatusDTO
            {
                ID = orderstatus.ID,
                Description = orderstatus.Description
            };

        }

        public static IList<OrderStatusDTO> ToOrderStatusBusiness(this IEnumerable<OrderStatu> orderstatus)
        {
            if (orderstatus == null || !orderstatus.Any()) { return null; }

            return orderstatus.Select(e => e.ToOrderStatusBusiness()).ToList();

        }

        public static OrderStatu ToOrderStatusEntity(this OrderStatusDTO orderstatus)
        {
            if (orderstatus == null) { return null; }
            return new OrderStatu
            {
                ID = orderstatus.ID,
                Description = orderstatus.Description
            };
        }

        public static IEnumerable<OrderStatu> ToOrderStatusEntity(this IList<OrderStatusDTO> orderstatus)
        {
            if (orderstatus == null || !orderstatus.Any()) { return null; }
            return orderstatus.Select(e => e.ToOrderStatusEntity()).ToList();
        }
    }
}

