using EasyShopping.Api.Models;
using EasyShopping.BusinessLogic.Business;
using EasyShopping.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace EasyShopping.Api.Controllers
{
    public class ProductController : ApiController
    {
        ProductBusinessLogic _business;
        public ProductController()
        {
            _business = new ProductBusinessLogic();
        }
        //public Task<IEnumerable<ProductApiModel>> Get()
        //{

        //}

        public ProductApiModel Post([FromBody] ProductApiModel data, int storeid)
        {

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
           
            var httpRequest = HttpContext.Current.Request;
            if(httpRequest.Files.Count <= 0)
            {
                return null;
            }
            IList<string> imgList = null;
            foreach (string file in httpRequest.Files)
            {
                //HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.Created);

                var postedFile = httpRequest.Files[file];
                
                if(postedFile.ContentLength < 0 || postedFile != null)
                {
                    //IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".png", ".gif" };
                    //var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                    //var extension = ext.ToLower();
                    //if (!AllowedFileExtensions.Contains(extension))
                    //{
                    //    return Request.CreateResponse(HttpStatusCode.BadRequest, "Please upload .jpg, .gif, .png only");
                    //}
                    if(!Directory.Exists(HttpContext.Current.Server.MapPath("/Img/Product/" + storeid)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Img/Product/" + storeid));
                    }
                    string root = HttpContext.Current.Server.MapPath("~/Img/Product/" + storeid + "/");
                    string path = Path.Combine(root, postedFile.FileName);
                    postedFile.SaveAs(path);
                    //TODO: call business function, send data and img path
                    imgList.Add(path);
                }
            }
            ProductDTO newproduct = _business.Add(ApiTranslators.Translate<ProductApiModel, ProductDTO>(data), imgList);
            return ApiTranslators.Translate<ProductDTO, ProductApiModel>(newproduct);
        }
    }
}