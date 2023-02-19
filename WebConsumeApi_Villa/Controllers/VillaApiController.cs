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
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
           return Ok(VillaStore.VillaList);
        }
        [HttpGet("{id:int}")]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if(id == 0) {
                return BadRequest();
            }
            var villa = VillaStore.VillaList.FirstOrDefault(x => x.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
             return Ok(villa);
        }
    }
}
