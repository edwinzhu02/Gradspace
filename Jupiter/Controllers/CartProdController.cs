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
    public class CartProdController : BaseController
    {
        public IHttpActionResult Get()
        {
            var result = new Result<List<CartProdModel>>();
            using (var db=  new jupiterEntities())
            {
                List<CartProdModel> cardProdModels = new List<CartProdModel>();
                List <CartProd> cartProds = db.CartProds.ToList();
                Mapper.Map(cartProds, cardProdModels);
                result.Data = cardProdModels;
                return Json(result);
            }
        }

        public IHttpActionResult Get(int id)
        {
            var result = new Result<CartProdModel>();
            CartProdModel cartProdModel = new CartProdModel();
            using (var db = new jupiterEntities())
            {
                var a = db.CartProds.Where(x => x.ID == id).Select(x=>x).FirstOrDefault();
                if (a == null)
                {
                    return Json(NotFound(result));
                }
                Mapper.Map(a, cartProdModel);
                result.Data = cartProdModel;
                return Json(result);
            }
        }

        //add
        [ResultFilter]
        public IHttpActionResult Post([FromBody] CartProdModel cartProdModel)
        {
            var result = new Result<String>();
            using (var db = new jupiterEntities())
            {
                CartProd cartProd = new CartProd();
                Mapper.Map(cartProdModel, cartProd);
                try
                {
                    db.CartProds.Add(cartProd);
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
        }
        //update
        [ResultFilter]
        public IHttpActionResult Put(int id, [FromBody] CartProdModel cartProdModel)
        {
            var result = new Result<String>();
            Type type = typeof(CartProd);
            using (var db = new jupiterEntities())
            {
                var a = db.CartProds.Where(x => x.ID==id).Select(x => x).FirstOrDefault();
                if (a == null)
                {
                    return Json(NotFound(result));
                }
                UpdateTable(cartProdModel,type,a);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = e.Message;
                    return Json(result);
                }
                return Json(result);
            }
        }
    }
}
