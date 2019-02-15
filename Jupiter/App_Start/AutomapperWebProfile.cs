using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jupiter.Models;
using JupiterEntity;

namespace Jupiter
{
    public class AutomapperWebProfile:AutoMapper.Profile
    {
        public AutomapperWebProfile()
        {
            CreateMap<Cart, CartModel>();

        }

        public static void Run()
        {
            AutoMapper.Mapper.Initialize(x=>
            {
                x.AddProfile<AutomapperWebProfile>();
            });
        }

    }
}