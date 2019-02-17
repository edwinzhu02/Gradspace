using System;
using System.Collections.Generic;
using System.Linq;
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

                //AutoMapper.Mapper.Map(carts, cartModels);

                

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
                var carts = db.Carts.Where(x => x.CartID == id).Select(x =>
                        new CartModel
                        {
                            CartId = x.CartID,
                            Price = x.Price,
                            Location = x.Location,
                            PlannedTime = x.PlannedTime,
                            IsActivate = x.IsActivate,
                            CreateOn = x.CreateOn,
                            UpdateOn = x.UpdateOn,
                            ContactId = x.ContactId
                        }).FirstOrDefault();
                result.Data = carts;
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
                //{
                //    CartID = newCart.CartId,
                //    Price = newCart.Price,
                //    Location = newCart.Location,
                //    PlannedTime = newCart.PlannedTime,
                //    IsActivate = newCart.IsActivate,
                //    CreateOn = newCart.CreateOn,
                //    UpdateOn = newCart.UpdateOn,
                //    ContactId = newCart.ContactId,
                //};
                AutoMapper.Mapper.Map(newCart, newDb);
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
                updated.Price = cartModel.Price ?? updated.Price;
                updated.Location = cartModel.Location ?? updated.Location;
                updated.PlannedTime = cartModel.PlannedTime ?? updated.PlannedTime;
                updated.IsActivate = cartModel.IsActivate ?? updated.IsActivate;
                updated.CreateOn = cartModel.CreateOn ?? updated.CreateOn;
                updated.UpdateOn = cartModel.UpdateOn ?? updated.UpdateOn;
                updated.ContactId = cartModel.ContactId ?? updated.ContactId;
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