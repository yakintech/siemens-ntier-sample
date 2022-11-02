using AutoMapper;
using Siemens.DAL.ORM.Entity;
using Siemens.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.Mapping.Models
{
    public class CreateRequestProductProfile : Profile
    {

        public CreateRequestProductProfile()
        {

            CreateMap<CreateProductRequestDto, Product>()
                .ForMember(dest => dest.UnitsInStock, opt => opt.MapFrom(src => src.Stock))
                .AfterMap((_, dest) =>
                {
                    dest.UnitPrice = 100;
                });
                
        }
    }
}
