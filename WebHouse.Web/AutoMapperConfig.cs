using AutoMapper;
using WebHouse.Web.Models.DTO;

namespace WebHouse.Web
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<VillaDTO,VillaCreateDTO>().ReverseMap();
            CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();
           
            CreateMap<VillaNumberDTO,VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();
           


        }
    }
}
