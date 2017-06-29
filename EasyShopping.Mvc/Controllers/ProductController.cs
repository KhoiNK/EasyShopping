using EasyShopping.BusinessLogic.Business;
using EasyShopping.BusinessLogic.Models;
using EasyShopping.Mvc.Models;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace EasyShopping.Mvc.Controllers
{
    public class ProductController : Controller
    {
        ProductBusinessLogic _business;
        public ProductController()
        {
            _business = new ProductBusinessLogic();
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            return View("AddProduct");
        }

        [HttpPost]
        public ActionResult AddProduct(ProductViewModel data, int storeid = 1)
        {
            //if (!Request.Content.IsMimeMultipartContent())
            //{
            //    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            //}

            var httpRequest = Request;
            if (httpRequest.Files.Count <= 0)
            {
                return null;
            }
            IList<string> imgList = null;
            foreach (string file in httpRequest.Files)
            {
                //HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.Created);

                var postedFile = httpRequest.Files[file];

                if (postedFile.ContentLength < 0 || postedFile != null)
                {
                    //IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".png", ".gif" };
                    //var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                    //var extension = ext.ToLower();
                    //if (!AllowedFileExtensions.Contains(extension))
                    //{
                    //    return Request.CreateResponse(HttpStatusCode.BadRequest, "Please upload .jpg, .gif, .png only");
                    //}
                    if (!Directory.Exists(Server.MapPath("/Img/Product/" + storeid)))
                    {
                        Directory.CreateDirectory(Server.MapPath("/Img/Product/" + storeid));
                    }
                    string root = Server.MapPath("~/Img/Product/" + storeid + "/");
                    string path = Path.Combine(root, postedFile.FileName);
                    postedFile.SaveAs(path);
                    //TODO: call business function, send data and img path
                    imgList.Add(path);
                }
            }
            ProductDTO newproduct = _business.Add(ViewModelTranslator.Translate<ProductViewModel, ProductDTO>(data));
            return Redirect("/Product/AddProduct");
        }
    }
}