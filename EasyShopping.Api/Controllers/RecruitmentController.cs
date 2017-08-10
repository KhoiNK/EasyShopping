using EasyShopping.Api.Models;
using EasyShopping.BusinessLogic.Business;
using EasyShopping.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EasyShopping.Api.Controllers
{

    [Authorize]
    public class RecruitmentController : ApiController
    {
        RecruitmentBusinessLogic _business;
        public RecruitmentController()
        {
            _business = new RecruitmentBusinessLogic();
        }

        public IHttpActionResult Post([FromBody] RecruitmentApiModel data)
        {
            var result = _business.Create(ApiTranslators.Translate<RecruitmentApiModel, RecruitmentDTO>(data));
            return Ok(result);
        }

        public IHttpActionResult Put([FromBody] RecruitmentApiModel data)
        {
            var result = _business.Update(ApiTranslators.Translate<RecruitmentApiModel, RecruitmentDTO>(data));
            return Ok(result);
        }

        public IHttpActionResult Get(int id)
        {
            var result = _business.GetByStore(id);
            return Ok(result);
        }
    }
}
