using EasyShopping.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.Api.Models.District
{
    public static class DistrictTranslator
    {
        public static DistrictApiModel ToDistrictApi(this DistrictDTO district)
        {
            if (district == null) { return null; }

            return new DistrictApiModel
            {
                Id = district.Id,
                Name = district.Name,
                Type = district.Type,
                LatiLongTude = district.LatiLongTude,
                ProvinceId = district.ProvinceId,
                SortOrder = district.SortOrder,
                IsDeleted = district.IsDeleted,
                IsPublished = district.IsPublished
            };

        }

        public static IList<DistrictApiModel> ToDistrictApi(this IEnumerable<DistrictDTO> districts)
        {
            if (districts == null || !districts.Any()) { return null; }

            return districts.Select(e => e.ToDistrictApi()).ToList();

        }

        public static DistrictDTO ToDistrictDTO(this DistrictApiModel district)
        {
            if (district == null) { return null; }
            return new DistrictDTO
            {
                Id = district.Id,
                Name = district.Name,
                Type = district.Type,
                LatiLongTude = district.LatiLongTude,
                ProvinceId = district.ProvinceId,
                SortOrder = district.SortOrder,
                IsDeleted = district.IsDeleted,
                IsPublished = district.IsPublished
            };
        }

        public static IEnumerable<DistrictDTO> ToDistrictApi(this IList<DistrictApiModel> districts)
        {
            if (districts == null || !districts.Any()) { return null; }
            return districts.Select(e => e.ToDistrictDTO()).ToList();
        }
    }
}
