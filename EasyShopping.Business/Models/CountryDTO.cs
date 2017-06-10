using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public class CountryDTO
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string CommonName { get; set; }
        public string FormalName { get; set; }
        public string CountryType { get; set; }
        public string CountrySubType { get; set; }
        public string Sovereignty { get; set; }
        public string Capital { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string TelephoneCode { get; set; }
        public string CountryCode3 { get; set; }
        public string CountryNumber { get; set; }
        public string InternetCountryCode { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<bool> IsPublished { get; set; }
        public string Flags { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}