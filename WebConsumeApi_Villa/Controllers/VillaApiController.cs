using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebConsumeApi_Villa.Models;

namespace WebConsumeApi_Villa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Villa> GetVillas()
        {
            return new List<Villa>
            {
                new Villa {Id=1,Name="salamu"},
                new Villa {Id=2,Name="saif"},
                new Villa {Id=3,Name="shahrukh"},
               
            };
        }
    }
}
