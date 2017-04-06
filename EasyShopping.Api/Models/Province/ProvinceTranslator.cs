using EasyShopping.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.Api.Models.Province
{
    public static class ProvinceTranslator
    {
        public static ProvinceApiModel ToProvinceApi(this ProvinceDTO province)
        {
            if (province == null) { return null; }

            return new ProvinceApiModel
            {
                Id = province.Id,
                Name = province.Name,
                Type = province.Type,
                TelephoneCode = province.TelephoneCode,
                ZipCode = province.ZipCode,
                CountryId = province.CountryId,
                CountryCode = province.CountryCode,
                SortOrder = province.SortOrder,
                IsPublished = province.IsPublished,
                IsDeleted = province.IsDeleted
            };
        }

        public static IList<ProvinceApiModel> ToProvinceApi(this IEnumerable<ProvinceDTO> provinces)
        {
            if (provinces == null || !provinces.Any()) { return null; }

            return provinces.Select(e => e.ToProvinceApi()).ToList();

        }

        public static ProvinceDTO ToProvinceDTO(this ProvinceApiModel province)
        {
            if (province == null) { return null; }
            return new ProvinceDTO
            {
                Id = province.Id,
                Name = province.Name,
                Type = province.Type,
                TelephoneCode = province.TelephoneCode,
                ZipCode = province.ZipCode,
                CountryId = province.CountryId,
                CountryCode = province.CountryCode,
                SortOrder = province.SortOrder,
                IsPublished = province.IsPublished,
                IsDeleted = province.IsDeleted
            };
        }

        public static IEnumerable<ProvinceDTO> ToProvinceApi(this IList<ProvinceApiModel> provinces)
        {
            if (provinces == null || !provinces.Any()) { return null; }
            return provinces.Select(e => e.ToProvinceDTO()).ToList();
        }
    }
}
