using Easyshopping.DataAccess.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public static class DisctrictTranslator
    {
        public static DistrictDTO ToDistrictBusiness(this District district)
        {
            if (district == null) { return null; }

            return new DistrictDTO
            {
                Id = district.Id,
                IsDeleted = district.IsDeleted,
                IsPublished = district.IsPublished,
                LatiLongTude = district.LatiLongTude,
                Name = district.Name,
                ProvinceId = district.ProvinceId,
                SortOrder = district.SortOrder,
                Type = district.Type
            };

        }

        public static IList<DistrictDTO> ToDistrictBusiness(this IEnumerable<District> districts)
        {
            if (districts == null || !districts.Any()) { return null; }

            return districts.Select(e => e.ToDistrictBusiness()).ToList();

        }

        public static District ToDistrictEntity(this DistrictDTO district)
        {
            if (district == null) { return null; }
            return new District
            {
                Id = district.Id,
                IsDeleted = district.IsDeleted,
                IsPublished = district.IsPublished,
                LatiLongTude = district.LatiLongTude,
                Name = district.Name,
                ProvinceId = district.ProvinceId,
                SortOrder = district.SortOrder,
                Type = district.Type
            };
        }

        public static IEnumerable<District> ToDistrictEntity(this IList<DistrictDTO> districts)
        {
            if (districts == null || !districts.Any()) { return null; }
            return districts.Select(e => e.ToDistrictEntity()).ToList();
        }
    }
}