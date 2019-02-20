using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;
using Jupiter.Models;
using JupiterEntity;
using Newtonsoft.Json;


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
                AutoMapper.Mapper.Map(productCategories, productCategoryModels);
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
                    return Json(NotFound(result));
                }
                ProductCategoryModel productCategoryModel = new ProductCategoryModel();
                AutoMapper.Mapper.Map(a, productCategoryModel);
                result.Data = productCategoryModel;
                return Json(result);
            }
        }
            //add
            public IHttpActionResult Post([FromBody] ProductCategoryModel _productCategory)
        {
            var result = new Result<string>();
            result = base.CheckStateModel(ModelState);
            if (result.IsSuccess == false) return Json(result);
            using (var db = new jupiterEntities())
            {
                ProductCategory cate = new ProductCategory
                {
                    //CategroyId = _productCategory.Id,
                    CategroyName = _productCategory.CategoryName
                };
                try
                {
                    db.ProductCategories.Add(cate);
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
        public IHttpActionResult Put(int id, [FromBody]ProductCategory _newCategpty)
        {
            var result = new Result<string>();
            //??????????
            result = base.CheckStateModel(ModelState);
            if (result.IsSuccess == false) return Json(result);
            using (var dbContext = new jupiterEntities())
            {
                ProductCategory updatedCate = dbContext.ProductCategories.Where(x => x.CategroyId == id).Select(x => x).FirstOrDefault();
                if (updatedCate == null)
                {
                    result.IsFound = true;
                    result.ErrorMessage = "Not Found";
                    return Json(result);
                }
                updatedCate.CategroyName = _newCategpty.CategroyName ?? updatedCate.CategroyName;
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
            result = base.CheckStateModel(ModelState);
            using (var db = new jupiterEntities())
            {
                ProductCategory del = db.ProductCategories.FirstOrDefault(x => x.CategroyId == id);
                if (del == null)
                {
                    result.ErrorMessage = "Not Found";
                    return Json(result);
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