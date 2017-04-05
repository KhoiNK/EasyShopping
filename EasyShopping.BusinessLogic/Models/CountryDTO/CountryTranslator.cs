using Easyshopping.DataAccess.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public static class CountryTranslator
    {
        public static CountryDTO ToCountryBusiness(this Country country)
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

        public static IList<CountryDTO> ToCountryBusiness(this IEnumerable<Country> countries)
        {
            if (countries == null || !countries.Any()) { return null; }

            return countries.Select(e => e.ToCountryBusiness()).ToList();

        }

        public static Country ToCountryEntity(this CountryDTO country)
        {
            if (country == null) { return null; }
            return new Country
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

        public static IEnumerable<Country> ToCountryEntity(this IList<CountryDTO> countries)
        {
            if (countries == null || !countries.Any()) { return null; }
            return countries.Select(e => e.ToCountryEntity()).ToList();
        }
    }
}