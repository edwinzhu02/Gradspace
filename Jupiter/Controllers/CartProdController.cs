using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
                AutoMapper.Mapper.Map(cartProds, cardProdModels);
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
                var a = db.CartProds.Where(x => x.CartId == id).Select(x=>x).FirstOrDefault();
                if (a == null)
                {
                    result.ErrorMessage = "Not found";
                    result.IsSuccess = false;
                    return Json(result);
                }
                AutoMapper.Mapper.Map(a, cartProdModel);
                result.Data = cartProdModel;
                return Json(result);
            }
        }

        //public IHttpActionResult Post([FromBody] CartProdModel cartProdModel)
        //{

        //}
    }
}
