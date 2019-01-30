using AutoMapper;
using Jupiter.Models;
using JupiterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jupiter.Common
{
    static public class AutoMapperCfg
    {
        static public IMapper mapper;
        static public void Config()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductModel>());
            mapper = config.CreateMapper();
        }

    }
}