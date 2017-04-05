using Easyshopping.DataAccess.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public static class CategoryTranslator
    {
        public static CategoryDTO ToCategoryBusiness(this Category category)
        {
            if (category == null) { return null; }

            return new CategoryDTO
            {
                ID = category.ID,
                Name = category.Name
            };

        }

        public static IList<CategoryDTO> ToCountryBusiness(this IEnumerable<Category> categories)
        {
            if (categories == null || !categories.Any()) { return null; }

            return categories.Select(e => e.ToCategoryBusiness()).ToList();

        }

        public static Category ToCategoryEntity(this CategoryDTO category)
        {
            if (category == null) { return null; }
            return new Category
            {
                ID = category.ID,
                Name = category.Name
            };
        }

        public static IEnumerable<Category> ToCategoryEntity(this IList<CategoryDTO> countries)
        {
            if (countries == null || !countries.Any()) { return null; }
            return countries.Select(e => e.ToCategoryEntity()).ToList();
        }
    }
}