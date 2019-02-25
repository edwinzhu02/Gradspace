using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Jupiter.ActionFilter;
using Jupiter.Models;
using JupiterEntity;

namespace Jupiter.Controllers
{
    public class ProductCategoryController : BaseController
    {
        // GET: ProductCategory
        public IHttpActionResult Get()
        {
            var result = new Result<List<ProductCategoryModel>>();
            using( var db = new jupiterEntities())
            {
                List<ProductCategoryModel> productCategoryModels = new List<ProductCategoryModel>();
                List<ProductCategory> productCategories = db.ProductCategories.ToList();
                Mapper.Map(productCategories, productCategoryModels);
                result.Data = productCategoryModels;
                return Json(result);
            }
        }

        public IHttpActionResult Get(int id)
        {
            var result = new Result<ProductCategoryModel>();
            using (var db = new jupiterEntities())
            {
                var a = db.ProductCategories.Where(x => x.CategroyId == id).Select(x => x).FirstOrDefault();
                if (a == null)
                {
                    return Json(DataNotFound(result));
                }
                ProductCategoryModel productCategoryModel = new ProductCategoryModel();
                Mapper.Map(a, productCategoryModel);
                result.Data = productCategoryModel;
                return Json(result);
            }
        }
            //add
        [CheckModelFilter]
        public IHttpActionResult Post([FromBody] ProductCategoryModel productCategory)
        {
            var result = new Result<string>();
            using (var db = new jupiterEntities())
            {
                ProductCategory proCategory = new ProductCategory();

                Mapper.Map(productCategory, proCategory);
                try
                {
                    db.ProductCategories.Add(proCategory);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = e.Message;
                }
                return Json(result);
            }
        }
        //update
        [CheckModelFilter]
        public IHttpActionResult Put(int id, [FromBody]ProductCategoryModel upCategory)
        {
            var result = new Result<string>();
            using (var dbContext = new jupiterEntities())
            {
                ProductCategory updatedCate = dbContext.ProductCategories.Where(x => x.CategroyId == id).Select(x => x).FirstOrDefault();
                if (updatedCate == null)
                {
                    return Json(DataNotFound(result));
                }
                Type type = typeof(ProductCategory);
                UpdateTable(upCategory,type,updatedCate);
                try
                {
                    dbContext.SaveChanges();
                }
                catch(Exception e)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = e.Message;
                }
                return Json(result);
            }
        }
        public IHttpActionResult Delete(int id)
        {
            var result = new Result<string>();
            using (var db = new jupiterEntities())
            {
                ProductCategory del = db.ProductCategories.FirstOrDefault(x => x.CategroyId == id);
                if (del == null)
                {
                    return Json(DataNotFound(result));
                }
                try
                {
                    db.ProductCategories.Remove(del);
                    db.SaveChanges();
                }
                catch(Exception e)
                {
                    result.ErrorMessage = e.Message;
                    result.IsSuccess = false;
                }
                return Json(result);
            }
        }
    }
}