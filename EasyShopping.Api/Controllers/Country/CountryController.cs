using EasyShopping.Api.Models.Country;
using EasyShopping.BusinessLogic.Business.CountryLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.Api.Controllers.Country
{
    public class CountryController
    {
        public IEnumerable<CountryApiModel> Get()
        {
            CountryBusinessLogic _business = new CountryBusinessLogic();
            return _business.GetAll().ToCountryApi();
        }

    }
}