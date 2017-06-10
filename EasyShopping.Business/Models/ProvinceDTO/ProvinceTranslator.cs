using Easyshopping.DataAccess.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public static class ProvinceTranslator
    {
        public static ProvinceDTO ToProvinceBusiness(this Province province)
        {
            if (province == null) { return null; }

            return new ProvinceDTO
            {
                Id = province.Id,
                TelephoneCode = province.TelephoneCode,
                IsDeleted = province.IsDeleted,
                IsPublished = province.IsPublished,
                ZipCode = province.ZipCode,
                CountryCode = province.CountryCode,
                Name = province.Name,
                CountryId = province.CountryId,
                SortOrder = province.SortOrder,
                Type = province.Type
            };

        }

        public static IList<ProvinceDTO> ToProvinceBusiness(this IEnumerable<Province> provinces)
        {
            if (provinces == null || !provinces.Any()) { return null; }

            return provinces.Select(e => e.ToProvinceBusiness()).ToList();

        }

        public static Province ToProvinceEntity(this ProvinceDTO province)
        {
            if (province == null) { return null; }
            return new Province
            {
                Id = province.Id,
                TelephoneCode = province.TelephoneCode,
                IsDeleted = province.IsDeleted,
                IsPublished = province.IsPublished,
                ZipCode = province.ZipCode,
                CountryCode = province.CountryCode,
                Name = province.Name,
                CountryId = province.CountryId,
                SortOrder = province.SortOrder,
                Type = province.Type
            };
        }
    }
}
    
