using AutoMapper;
using BeerCollectionBLL.DTO;
using BeerCollectionDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCollectionBLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Beer, BeerDto>().ReverseMap();
            CreateMap<Beer, BeerCreatedDto>().ReverseMap();

        }
    }
}
