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
    [Route("api/VillaApi")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        private readonly ILogger<VillaApiController> _logger;
        private readonly ILogging _loggerCustome;
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IVillaRepository _villaRepository;
        private readonly APIResponse _apiresponse;
        public VillaApiController(ILogger<VillaApiController> logger, ILogging loggerCustome, ApplicationDBContext dbContext, IMapper mapper, IVillaRepository villaRepository)
        {
            _logger = logger;
            _loggerCustome = loggerCustome;
            _dbContext = dbContext;
            _mapper = mapper;
            _villaRepository = villaRepository;
            this._apiresponse = new();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                IEnumerable<Villa> villasList = await _villaRepository.GetAllAsync();
                _apiresponse.Result = _mapper.Map<List<VillaDTO>>(villasList);
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
        [HttpGet("{id:int}",Name =("GetVilla"))]
        // [ProducesResponseType(200,Type=typeof(VillaDTO))]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
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
                var villa = await _villaRepository.GetAsync(x => x.Id == id);
                if (villa == null)
                {
                    _apiresponse.StatusCode = HttpStatusCode.NotFound;
                    _apiresponse.IsSuccess = false;
                    return NotFound(_apiresponse);
                }
                _apiresponse.Result = _mapper.Map<VillaDTO>(villa);
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
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDTO CreateDTOvilla)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _villaRepository.GetAsync(x => x.Name.ToLower() == CreateDTOvilla.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("CustomError", $"{CreateDTOvilla.Name} already Exists");
                    _apiresponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiresponse.IsSuccess = false;
                    return BadRequest(_apiresponse);
                }
                if (CreateDTOvilla == null)
                {
                    _apiresponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiresponse.IsSuccess = false;
                    return BadRequest(_apiresponse);
                }
                Villa villa = _mapper.Map<Villa>(CreateDTOvilla);

                await _villaRepository.CreateAsync(villa);
                _apiresponse.Result = _mapper.Map<VillaDTO>(villa);
                _apiresponse.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVilla", new { id = villa.Id }, _apiresponse);
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
        [HttpDelete("{id:int}",Name =("DeleteVilla"))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _apiresponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiresponse);
                }
                var villa = await _villaRepository.GetAsync(u => u.Id == id);
                if (villa == null)
                {
                    _apiresponse.StatusCode = HttpStatusCode.NotFound;
                    _apiresponse.IsSuccess = false;
                    return NotFound(_apiresponse);
                }
                await _villaRepository.RemoveAsync(villa);
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
        [HttpPut("{id:int}", Name = ("UpdateVilla"))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]      
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDTO UpdateDTOvill)
        {
            try
            {
                if (UpdateDTOvill == null || id != UpdateDTOvill.Id)
                {
                    _apiresponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiresponse.IsSuccess = false;
                    return BadRequest(_apiresponse);
                }
                var model = _mapper.Map<Villa>(UpdateDTOvill);
                await _villaRepository.UpdateAsync(model);
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

        [HttpPatch("{id:int}", Name = ("UpdatePartialVilla"))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> villaDTO)
        {
            if (villaDTO == null || id ==0)
            {
                return BadRequest();
            }
            var villa = await _villaRepository.GetAsync(x => x.Id == id,tracked:false);
            VillaUpdateDTO modelDTO = _mapper.Map<VillaUpdateDTO>(villa);
          
            if (villa == null)
            {
                return BadRequest();
            }
            villaDTO.ApplyTo(modelDTO, ModelState);
            Villa model = _mapper.Map<Villa>(modelDTO);

           await _villaRepository.UpdateAsync(model);
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
