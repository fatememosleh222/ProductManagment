using AutoMapper;
using Product.Common.DTOs.Common;
using Product.Common.DTOs.Security;
using Product.Domain.Common;
using Product.Domain.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Services.AutoMapperConfig
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<ProductsDTO, Products>().ReverseMap();

        }
    }
}
