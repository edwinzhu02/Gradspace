using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Jupiter.Models;
using JupiterEntity;

namespace Jupiter.Controllers
{
    public class CartController : BaseController
    {
        // GET api/values
        public IHttpActionResult Get()
        {
            var result = new Result<List<CartModel>>();
            using (var db = new jupiterEntities())
            {
                List<Cart> carts = db.Carts.ToList();
                List<CartModel> cartModels = new List<CartModel>();
                Mapper.Map(carts, cartModels);
                result.Data = cartModels;
                return Json(result);
            }

        }
        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            var result = new Result<CartModel>();
            using (var db = new jupiterEntities())
            {
                var carts = db.Carts.Where(x => x.CartID == id).Select(x =>x).FirstOrDefault();
                CartModel cartModel = new CartModel();
                Mapper.Map(carts, cartModel);
                result.Data = cartModel;
                if (carts == null)
                {
                    result.IsFound = false;
                    return Json(result);
                }
                return Json(result);
            }
        }

        // POST api/values
        public IHttpActionResult Post([FromBody]CartModel newCart)
        {
            var result = CheckStateModel(ModelState);
            if (result.IsSuccess == false)
            {
                return Json(result);
            }
            using (var db = new jupiterEntities())
            {
                Cart newDb = new Cart();

                Mapper.Map(newCart, newDb);
                try
                {
                    db.Carts.Add(newDb);
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

        // PUT api/values/5
        public IHttpActionResult Put(int id, [FromBody]CartModel cartModel)
        {
            var result = CheckStateModel(ModelState);
            var properties = cartModel.GetType().GetProperties();
            Type cartType = typeof(Cart);
            if (result.IsSuccess == false)
            {
                return Json(result);
            }
            using (var db = new jupiterEntities())
            {
                Cart updated = db.Carts.Where(x => x.CartID == id).Select(x => x).FirstOrDefault();
                if (updated == null)
                {
                    result.IsFound = false;
                    return Json(result);
                }
                foreach (var prop in properties)
                {
                    PropertyInfo piInstance = cartType.GetProperty(prop.Name);
                    if (piInstance != null && prop.GetValue(cartModel) != null)
                    {
                        piInstance.SetValue(updated,prop.GetValue(cartModel));
                    }           
                }
                try
                {
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
        // DELETE api/values/5
        public IHttpActionResult Delete(int id)
        {
            var result = new Result<string>();
            using (var db = new jupiterEntities())
            {
                Cart del = db.Carts.Where(x => x.CartID == id).Select(x => x).FirstOrDefault();
                if (del == null)
                {
                    result.IsFound = false;
                    return Json(result);
                }
                try
                {
                    del.IsActivate = 0;
                    //db.Carts.Remove(del);
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