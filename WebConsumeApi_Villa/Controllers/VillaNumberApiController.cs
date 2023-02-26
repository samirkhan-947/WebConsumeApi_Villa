using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebConsumeApi_Villa.Data;
using WebConsumeApi_Villa.Logging;
using WebConsumeApi_Villa.Models;
using WebConsumeApi_Villa.Models.DTO;
using WebConsumeApi_Villa.Repository.IRepository;

namespace WebConsumeApi_Villa.Controllers
{
    [Route("api/VillaNumberApi")]
    [ApiController]
    public class VillaNumberApiController : ControllerBase
    {
        private readonly ILogger<VillaNumberApiController> _logger;
        private readonly ILogging _loggerCustome;
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IVillaNumberRepository _IVillaNumberRepository;
        private readonly IVillaRepository _villarepo;
        private readonly APIResponse _apiresponse;
        public VillaNumberApiController(ILogger<VillaNumberApiController> logger, ILogging loggerCustome, ApplicationDBContext dbContext, IMapper mapper, IVillaNumberRepository IVillaNumberRepository, IVillaRepository villarepo)
        {
            _logger = logger;
            _loggerCustome = loggerCustome;
            _dbContext = dbContext;
            _mapper = mapper;
            _IVillaNumberRepository = IVillaNumberRepository;
            this._apiresponse = new();
            _villarepo = villarepo;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumberVillas()
        {
            try
            {
                IEnumerable<VillaNumber> villasNumberList = await _IVillaNumberRepository.GetAllAsync();
                _apiresponse.Result = _mapper.Map<List<VillaNumberDTO>>(villasNumberList);
                _apiresponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiresponse);
            }
            catch (Exception ex)
            {
                _apiresponse.IsSuccess = false;
                _apiresponse.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
                
            }
            return _apiresponse;
        }
        [HttpGet("{id:int}",Name =("GetNumberVilla"))]
       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetNumberVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("This is bad reguest");
                    _loggerCustome.LogWithColor("This is bad reguest for custom", "error");
                    _apiresponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiresponse.IsSuccess = false;
                    return BadRequest(_apiresponse);
                }
                var villaNumber = await _IVillaNumberRepository.GetAsync(x => x.VillaNo == id);
                if (villaNumber == null)
                {
                    _apiresponse.StatusCode = HttpStatusCode.NotFound;
                    _apiresponse.IsSuccess = false;
                    return NotFound(_apiresponse);
                }
                _apiresponse.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _apiresponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiresponse);
            }
            catch (Exception ex)
            {

                _apiresponse.IsSuccess = false;
                _apiresponse.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _apiresponse;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateNumberVilla([FromBody] VillaNumberCreateDTO CreateDTOvilla)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _IVillaNumberRepository.GetAsync(x => x.VillaNo == CreateDTOvilla.VillaNo) != null)
                {
                    ModelState.AddModelError("CustomError", $"{CreateDTOvilla.VillaNo} already Exists");
                    _apiresponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiresponse.IsSuccess = false;
                    return BadRequest(_apiresponse);
                }
                if(await _villarepo.GetAsync(u=>u.Id== CreateDTOvilla.VillaID) == null)
                {
                    ModelState.AddModelError("CustomError","Villa ID is Invalid!");
                    return BadRequest(ModelState);
                }
                if (CreateDTOvilla == null)
                {
                    _apiresponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiresponse.IsSuccess = false;
                    return BadRequest(_apiresponse);
                }
                VillaNumber villa = _mapper.Map<VillaNumber>(CreateDTOvilla);

                await _IVillaNumberRepository.CreateAsync(villa);
                _apiresponse.Result = _mapper.Map<VillaNumberDTO>(villa);
                _apiresponse.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetNumberVilla", new { id = villa.VillaNo }, _apiresponse);
            }
            catch (Exception ex)
            {

                _apiresponse.IsSuccess = false;
                _apiresponse.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _apiresponse;
        }
        [HttpDelete("{id:int}",Name =("DeleteVillaNumber"))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _apiresponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiresponse);
                }
                var villa = await _IVillaNumberRepository.GetAsync(u => u.VillaNo == id);
                if (villa == null)
                {
                    _apiresponse.StatusCode = HttpStatusCode.NotFound;
                    _apiresponse.IsSuccess = false;
                    return NotFound(_apiresponse);
                }
                await _IVillaNumberRepository.RemoveAsync(villa);
                _apiresponse.StatusCode = HttpStatusCode.NoContent;
                _apiresponse.IsSuccess = true;
                return Ok(_apiresponse);
            }
            catch (Exception ex)
            {

                _apiresponse.IsSuccess = false;
                _apiresponse.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _apiresponse;
        }
        [HttpPut("{id:int}", Name = ("UpdateVillaNumber"))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]      
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDTO UpdateDTOvill)
        {
            try
            {
                if (UpdateDTOvill == null || id != UpdateDTOvill.VillaNo)
                {
                    _apiresponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiresponse.IsSuccess = false;
                    return BadRequest(_apiresponse);
                }
                if (await _villarepo.GetAsync(u => u.Id == UpdateDTOvill.VillaID) == null)
                {
                    ModelState.AddModelError("CustomError", "Villa ID is Invalid!");
                    return BadRequest(ModelState);
                }
                var model = _mapper.Map<VillaNumber>(UpdateDTOvill);
                await _IVillaNumberRepository.UpdateAsync(model);
                _apiresponse.StatusCode = HttpStatusCode.NoContent;
                _apiresponse.IsSuccess = true;
                return Ok(_apiresponse);
            }
            catch (Exception ex)
            {

                _apiresponse.IsSuccess = false;
                _apiresponse.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
           return _apiresponse;
        }

        [HttpPatch("{id:int}", Name = ("UpdatePartialVillaNumber"))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVillaNumber(int id, JsonPatchDocument<VillaNumberUpdateDTO> villaDTO)
        {
            if (villaDTO == null || id ==0)
            {
                return BadRequest();
            }
            var villa = await _IVillaNumberRepository.GetAsync(x => x.VillaNo == id,tracked:false);
            VillaNumberUpdateDTO modelDTO = _mapper.Map<VillaNumberUpdateDTO>(villa);
          
            if (villa == null)
            {
                return BadRequest();
            }
            villaDTO.ApplyTo(modelDTO, ModelState);
            VillaNumber model = _mapper.Map<VillaNumber>(modelDTO);

           await _IVillaNumberRepository.UpdateAsync(model);
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
