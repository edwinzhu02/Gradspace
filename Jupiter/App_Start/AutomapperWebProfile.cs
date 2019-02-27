using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            CreateMap<Product, ProcessModule>();
            CreateMap<ProcessModule, Product>().ForMember(x => x.ProductMedias, opt => opt.Ignore());
            CreateMap<Cart, CartModel>().ReverseMap();
            CreateMap<Contact, ContactModel>().ReverseMap();
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<CartProd, CartProdModel>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryModel>().ReverseMap();
            CreateMap<Testimonial,TestimonialModel>().ReverseMap();
            CreateMap<EventType, EventTypeModel>().ReverseMap();
            CreateMap<Project, ProjectModel>().ReverseMap();
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