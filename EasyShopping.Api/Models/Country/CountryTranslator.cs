using EasyShopping.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.Api.Models.Country
{
    public static class CountryTranslator
    {
        public static CountryApiModel ToCountryApi(this CountryDTO country)
        {
            if (country == null) { return null; }

            return new CountryApiModel
            {
                Capital = country.Capital,
                CommonName = country.CommonName,
                CountryCode = country.CountryCode,
                CountryCode3 = country.CountryCode3,
                CountryNumber = country.CountryNumber,
                CountrySubType = country.CountrySubType,
                CountryType = country.CountryType,
                CurrencyCode = country.CurrencyCode,
                CurrencyName = country.CurrencyName,
                Flags = country.Flags,
                FormalName = country.FormalName,
                Id = country.Id,
                InternetCountryCode = country.InternetCountryCode,
                IsDeleted = country.IsDeleted,
                IsPublished = country.IsPublished,
                SortOrder = country.SortOrder,
                Sovereignty = country.Sovereignty,
                TelephoneCode = country.TelephoneCode
            };

        }

        public static IList<CountryApiModel> ToCountryApi(this IEnumerable<CountryDTO> countries)
        {
            if (countries == null || !countries.Any()) { return null; }

            return countries.Select(e => e.ToCountryApi()).ToList();

        }

        public static CountryDTO ToCountryDTO(this CountryApiModel country)
        {
            if (country == null) { return null; }
            return new CountryDTO
            {
                Capital = country.Capital,
                CommonName = country.CommonName,
                CountryCode = country.CountryCode,
                CountryCode3 = country.CountryCode3,
                CountryNumber = country.CountryNumber,
                CountrySubType = country.CountrySubType,
                CountryType = country.CountryType,
                CurrencyCode = country.CurrencyCode,
                CurrencyName = country.CurrencyName,
                Flags = country.Flags,
                FormalName = country.FormalName,
                Id = country.Id,
                InternetCountryCode = country.InternetCountryCode,
                IsDeleted = country.IsDeleted,
                IsPublished = country.IsPublished,
                SortOrder = country.SortOrder,
                Sovereignty = country.Sovereignty,
                TelephoneCode = country.TelephoneCode
            };
        }

        public static IEnumerable<CountryDTO> ToCountryApi(this IList<CountryApiModel> countries)
        {
            if (countries == null || !countries.Any()) { return null; }
            return countries.Select(e => e.ToCountryDTO()).ToList();
        }
    }
}