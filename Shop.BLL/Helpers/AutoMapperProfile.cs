using AutoMapper;
using Shop.BLL.Models;
using Shop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.BLL.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDTO>();
        }
    }
}
