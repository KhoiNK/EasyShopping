using EasyShopping.Api.Models;
using EasyShopping.BusinessLogic.Business;
using EasyShopping.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EasyShopping.Api.Controllers
{
    public class CountryController : ApiController
    {
       // [Authorize(Roles = "CountryGet")] // OAuthorize(Name="CountryGet") => Filter => 404
        public IEnumerable<CountryApiModel> Get()
        {
            CountryBusinessLogic _business = new CountryBusinessLogic();
            return ApiTranslators.Translate<CountryDTO, CountryApiModel>(_business.GetAll());
        }

        public CountryApiModel Get(int id)
        {
            CountryBusinessLogic _business = new CountryBusinessLogic();
            CountryApiModel country = ApiTranslators.Translate<CountryDTO,CountryApiModel>(_business.GetById(id));
            return country;
        }

    }
}