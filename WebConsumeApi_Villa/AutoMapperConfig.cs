using AutoMapper;
using WebConsumeApi_Villa.Models;
using WebConsumeApi_Villa.Models.DTO;

namespace WebConsumeApi_Villa
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Villa,VillaDTO>().ReverseMap();
            CreateMap<Villa,VillaCreateDTO>().ReverseMap();
            CreateMap<Villa,VillaUpdateDTO>().ReverseMap();
        }
    }
}
