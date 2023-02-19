using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebConsumeApi_Villa.Data;
using WebConsumeApi_Villa.Models;
using WebConsumeApi_Villa.Models.DTO;

namespace WebConsumeApi_Villa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDTO> GetVillas()
        {
            return VillaStore.VillaList;
        }
        [HttpGet("{id:int}")]
        public VillaDTO GetVilla(int id)
        {
             return VillaStore.VillaList.FirstOrDefault(x => x.Id == id);
        }
    }
}
