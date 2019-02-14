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
                var a = db.CartProds.Select(x =>
                    new CartProdModel
                    {
                        Id = x.ID,
                        CartId = x.CartId,
                        ProdId = x.ProdId,
                        Price = x.Price,
                        Title = x.Title,
                        SubTitle = x.SubTitle,
                        Quantity = x.Quantity
                    }).ToList();
                result.Data = a;
                return Json(result);
            }
        }
    }
}
