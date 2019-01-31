using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using JupiterEntity;
using Jupiter.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Web;
using Jupiter.Common;
using AutoMapper;

namespace Jupiter.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    
    public class ProductController : BaseController
    {
        [Route("api/productbytype/{type:int}")]
        [HttpGet]
        public IHttpActionResult GetByType(int type)
        {
            var result = new Result<List<ProductModel>>();
            return Json(result);
        }
        [Route("api/product")]
        public IHttpActionResult Get() {
            var result = new Result<List<ProductModel>>();

            using (var db = new JupiterEntities()) {
                List<Product> originalProd = db.Products.Select(x => x).ToList();
                MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductModel>());
                IMapper mapper = config.CreateMapper();
                List<ProductModel> prodModelList = new List<ProductModel>();
                prodModelList = mapper.Map<List<ProductModel>>(originalProd);
                //List<ProductModel> pdmList = prodFrom.Select(Mapper.Map<Product, ProductModel>).ToList();

                result.Data = prodModelList;
                return Json(result);
            }
         }
        //[Route("api/product/{id:int}/{id2:int}/{id3}")]--//api/product/1/2/"2018-03-14"
        //public IHttpActionResult Get(int id, int id2, string id3)
        //[Route("api/Task")]   ---api/Task?id=1&id2=2&id3="2018-03-14"
        [Route("api/product")]
        //api/product?queryType=1&type=1"
        public IHttpActionResult Get(int queryType,int type)
        {
            var result = new Result<ProductModel>();
            return Json(result);
        }
        [Route("api/product")]
        // GET: api/Product/5
        public IHttpActionResult Get(int id)
        {
            var result = new Result<ProductModel>();

            using (var db = new JupiterEntities())
            {
                var originalProd = db.Products.Where(p => p.ProdId == id).Select(x => x).FirstOrDefault();
                ProductModel prodModel = AutoMapperCfg.mapper.Map<ProductModel>(originalProd);
                result.Data = prodModel;
                if (originalProd != null) {
                    result.IsFound = false;
                }
                return Json(result);
            }
        }
        // POST: api/Product

        public IHttpActionResult Post([FromBody]ProductModel productModel)
        {
            var result = new Result<string>();
            result = base.CheckStateModel(ModelState);
            if (result.IsSuccess == false) return Json(result);

            using (var db = new JupiterEntities())
            {
                Product newProd = new Product
                {
                    ProdTypeId = productModel.ProdTypeId,
                    CategroyId = productModel.CategroyId,
                    Title = productModel.Title,
                    SubTitle = productModel.SubTitle,
                    Description = productModel.Description,
                    TotalStock = productModel.TotalStock,
                    AvailableStock = productModel.AvailableStock,
                    Price = productModel.Price,
                    SpcOrDisct = productModel.SpcOrDisct,
                    Discount = productModel.Discount,
                    SpecialPrice = productModel.SpecialPrice,
                    IsActivate = 1,
                    CreateOn = DateTime.Now
                };
                try
                {
                    foreach (var pm in productModel.ProdMedias)
                    {
                        ProductMedia prodMedia = new ProductMedia
                        {
                            url = pm
                        };
                        newProd.ProductMedias.Add(prodMedia);
                    }
                    db.Products.Add(newProd);
                    db.SaveChanges();
                    var prodId = newProd.ProdId;
                }
                catch (Exception e)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = e.Message;
                    }
                return Json(result);
            }
        }
        // PUT: api/Product/5
        public IHttpActionResult Put(int id, [FromBody]ProductModel productModel)
        {
            var result = new Result<string>();
            result = base.CheckStateModel(ModelState);
            if (result.IsSuccess == false) return Json(result);

            using (var db = new JupiterEntities())
            {
                Product updateProd = db.Products.Where(x => x.ProdId == id).
                    Select(x =>x).FirstOrDefault();
                if (updateProd == null) {
                    result.IsFound = false;
                    return Json(result);
                }
                updateProd.ProdTypeId = productModel.ProdTypeId ?? updateProd.ProdTypeId;
                updateProd.CategroyId = productModel.CategroyId ?? updateProd.CategroyId;
                updateProd.Title = productModel.Title ?? updateProd.Title;
                updateProd.SubTitle = productModel.SubTitle ?? updateProd.Title;
                updateProd.Description = productModel.Description ?? updateProd.Title;
                updateProd.TotalStock = productModel.TotalStock ?? updateProd.TotalStock;
                updateProd.AvailableStock = productModel.AvailableStock ?? updateProd.AvailableStock;
                updateProd.Price = productModel.Price ?? updateProd.Price;
                updateProd.SpcOrDisct = productModel.SpcOrDisct ?? updateProd.SpcOrDisct;
                updateProd.Discount = productModel.Discount ?? updateProd.Discount;
                updateProd.SpecialPrice = productModel.SpecialPrice ?? updateProd.SpecialPrice;
                updateProd.IsActivate = 1;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = e.Message;
                }
            }
            return Json(result);
        }
//test upload file
        [Route("api/productImg")]
       
        public async Task<HttpResponseMessage> SaveImg()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Resources");
            var provider = new FormDataStreamer(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server file path: " + file.LocalFileName);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
