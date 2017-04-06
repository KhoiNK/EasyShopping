using Easyshopping.DataAccess.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public static class PartnerTranslator
    {
        public static PartnerDTO ToPartnerBusiness(this Partner partner)
        {
            if (partner == null) { return null; }

            return new PartnerDTO
            {
                ID = partner.ID,
                StoreID = partner.StoreID,
                CreateDate = partner.CreatedDate,
                ModifiedDate = partner.ModifiedDate,
                ModifiedID = partner.ModifiedID,
                UserID = partner.UseID
            };
        }

        public static IList<PartnerDTO> ToPartnerBusiness(this IEnumerable<Partner> partners)
        {
            if (partners == null || !partners.Any()) { return null; }

            return partners.Select(e => e.ToPartnerBusiness()).ToList();
        }

        public static Partner ToPartnerEntity(this PartnerDTO partner)
        {
            if (partner == null) { return null; }
            return new Partner
            {
                ID = partner.ID,
                StoreID = partner.StoreID,
                CreateDate = partner.CreateDate,
                ModifiedDate = partner.ModifiedDate,
                ModifiedID = partner.ModifiedID,
                UserID = partner.UserID
            };
        }

        public static IEnumerable<Partner> ToPartnerEntity(this IList<PartnerDTO> partners)
        {
            if (partners == null || !partners.Any()) { return null; }
            return partners.Select(e => e.ToPartnerEntity()).ToList();
        }
    }
}

