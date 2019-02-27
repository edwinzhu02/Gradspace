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
using AutoMapper;
using Jupiter.ActionFilter;

namespace Jupiter.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    
    public class ProductController : BaseController
    {
        public IHttpActionResult Get() {
            var result = new Result<List<ProductModel>>();
            using (var db = new jupiterEntities())
            {
                List<Product> products = db.Products.ToList();
                List<ProductModel> productModels = new List<ProductModel>();
                Mapper.Map(products, productModels);
                result.Data = productModels;
                return Json(result);
            }

        }
        // sort products by type
        [Route("api/productbytype/{type:int}")]
        [HttpGet]
        public IHttpActionResult GetByType(int type)
        {
            var result = new Result<List<ProductModel>>();
            using (var db = new jupiterEntities())
            {
                List<Product> products = db.Products.Where(x => x.ProdTypeId == type).Select(x => x).ToList();
                List<ProductModel> productModels = new List<ProductModel>();
                Mapper.Map(products, productModels);
                result.Data = productModels;
                return Json(result);
            }
        }
        // GET: api/Product/5
        public IHttpActionResult Get(int id)
        {
            var result = new Result<ProductModel>();

            using (var db = new jupiterEntities())
            {
                Product product = db.Products.Find(id);
                ProductModel productModel = new ProductModel();
                if (product == null)
                {
                    return Json(DataNotFound(result));
                }
                Mapper.Map(product, productModel);
                result.Data = productModel;
                return Json(result);
            }
        }
        // POST: api/Product
        //add
        [CheckModelFilter]
        public IHttpActionResult Post([FromBody]ProductModel productModel)
        {
            var result = new Result<string>();  

            using (var db = new jupiterEntities())
            {
                Product product = new Product();
                productModel.CreateOn = DateTime.Now;
                Mapper.Map(productModel, product);
                try
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = e.Message;
                }
                try
                {
                    if (productModel.ProdMedias.Count != 0)
                    {
                        foreach (var pm in productModel.ProdMedias)
                        {
                            ProductMedia prodMedia = new ProductMedia();
                            prodMedia.ProdId = product.ProdId;
                            {
                                prodMedia.url = pm;
                            };
                            db.ProductMedias.Add(prodMedia);
                        }
                        db.SaveChanges();
                    }
                }
                catch ( Exception e)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = e.Message;
                }
                return Json(result);
            }
        }
        // PUT: api/Product/5
        //update
        [CheckModelFilter]
        public IHttpActionResult Put(int id, [FromBody]ProductModel productModel)
        {
            var result = new Result<string>();
            using (var db = new jupiterEntities())
            {
                Product updateProd = db.Products.Where(x => x.ProdId == id).Select(x =>x).FirstOrDefault();
                if (updateProd == null)
                {
                    return Json(DataNotFound(result));
                }

                Type prodType = typeof(Product);
                UpdateTable(productModel, prodType, updateProd);
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

        public IHttpActionResult Delete(int id)
        {
            var result = new  Result<string>();
            using (var db = new jupiterEntities())
            {
                var a = db.Products.Find(id);
                if (a == null)
                {
                    return Json(DataNotFound(result));
                }

                try
                {
                    a.IsActivate = 0;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    result.ErrorMessage = e.Message;
                    result.IsSuccess = false;
                }

                return Json(result);
            }
        }
    }
}
