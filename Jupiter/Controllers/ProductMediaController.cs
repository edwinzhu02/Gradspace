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
    public class ProductMediaController : BaseController
    {
        private jupiterEntities db = new jupiterEntities();

        // GET: api/ProductMedia
        public IHttpActionResult Get()
        {
            var result = new Result<List<ProductMediaModel>>();
            List<ProductMediaModel> productMediaModels = new List<ProductMediaModel>();
            List<ProductMedia> productMediae = db.ProductMedias.ToList();
            Mapper.Map(productMediae, productMediaModels);
            result.Data = productMediaModels;
            return Json(result);
        }

        // GET: api/ProductMedia/5
        public IHttpActionResult Get(int id)
        {
            var result = new Result<ProductMediaModel>();
                ProductMedia productMedia = db.ProductMedias.Find(id);
                ProductMediaModel productMediaModel = new ProductMediaModel();
                if (productMedia == null)
                {
                    return Json(DataNotFound(result));
                }

                Mapper.Map(productMedia, productMediaModel);
                result.Data = productMediaModel;
            return Json(result);
        }

        // PUT: api/ProductMedia/5
        [CheckModelFilter]
        public IHttpActionResult Put(int id, [FromBody]ProductMediaModel productMediaModel)
        {
            var result = new Result<string>();
                var a = db.ProductMedias.Where(x => x.Id == id).Select(x => x).FirstOrDefault();
                if (a == null)
                {
                    return Json(DataNotFound(result));
                }
                Type type = typeof(ProductMedia);
                UpdateTable(productMediaModel, type, a);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    result.ErrorMessage = e.Message;
                    result.IsSuccess = false;
                    return Json(result);
            }
            return Json(result);
        }
        // POST: api/ProductMedia
        [CheckModelFilter]
        public IHttpActionResult Post([FromBody]ProductMediaModel productMediaModel)
        {
            var result = new Result<ProductMediaModel>();
            ProductMedia productMedia = new ProductMedia();
                Mapper.Map(productMediaModel, productMedia);
                result.Data = productMediaModel;
                try
                {
                    db.ProductMedias.Add(productMedia);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    result.ErrorMessage = e.Message;
                    result.IsSuccess = false;
                    return Json(result);
                }
            return Json(result);
        }

        // DELETE: api/ProductMedia/5
        public IHttpActionResult Delete(int id)
        {
            var result = new Result<string>();
                ProductMedia productMedia = db.ProductMedias.Find(id);
                if (productMedia == null)
                {
                    return Json(DataNotFound(result));
                }
                try
                {
                    db.ProductMedias.Remove(productMedia);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    result.ErrorMessage = e.Message;
                    result.IsSuccess = false;
                    return Json(result);
                }
            return Json(result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}