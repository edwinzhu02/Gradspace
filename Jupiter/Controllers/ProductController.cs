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

namespace Jupiter.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    
    public class ProductController : BaseController
    {
        public IHttpActionResult Get() {
            var result = new Result<List<ProductModel>>();
            using (var db = new JupiterEntities()) {
                var prod = db.Products.Select(x =>
                new ProductModel { ProdId = x.ProdId,
                    ProdTypeId = x.ProdTypeId,
                    CategroyId = x.CategroyId,
                    Title = x.Title,
                    SubTitle = x.SubTitle,
                    Description = x.Description,
                    TotalStock = x.TotalStock,
                    AvailableStock = x.AvailableStock,
                    Price = x.Price,
                    SpcOrDisct = x.SpcOrDisct,
                    Discount = x.Discount,
                    ProdTypeName = x.ProductType.TypeName,
                    CategroyName = x.ProductCategory.CategroyName,
                    ProdMedias = x.ProductMedias.Select(pm => pm.url).ToList()
                }).ToList();
                result.Data = prod;
                return Json(result);
            }
         }
        // GET: api/Product/5
        public IHttpActionResult Get(int id)
        {
            var result = new Result<ProductModel>();

            using (var db = new JupiterEntities())
            {
                var prod = db.Products.Where(p=>p.ProdId==id).Select(x =>
                new ProductModel
                {
                    ProdId = x.ProdId,
                    ProdTypeId = x.ProdTypeId,
                    CategroyId = x.CategroyId,
                    Title = x.Title,
                    SubTitle = x.SubTitle,
                    Description = x.Description,
                    TotalStock = x.TotalStock,
                    AvailableStock = x.AvailableStock,
                    Price = x.Price,
                    SpcOrDisct = x.SpcOrDisct,
                    Discount = x.Discount,
                    ProdTypeName = x.ProductType.TypeName,
                    CategroyName = x.ProductCategory.CategroyName,
                    ProdMedias = x.ProductMedias.Select(pm => pm.url).ToList()
            }).FirstOrDefault();
                result.Data = prod;
                if (prod != null) {
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

    }
}
