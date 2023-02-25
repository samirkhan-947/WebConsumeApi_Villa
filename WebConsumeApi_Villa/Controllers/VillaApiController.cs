﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebConsumeApi_Villa.Data;
using WebConsumeApi_Villa.Logging;
using WebConsumeApi_Villa.Models;
using WebConsumeApi_Villa.Models.DTO;
using WebConsumeApi_Villa.Repository.IRepository;

namespace WebConsumeApi_Villa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        private readonly ILogger<VillaApiController> _logger;
        private readonly ILogging _loggerCustome;
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IVillaRepository _villaRepository;
        public VillaApiController(ILogger<VillaApiController> logger, ILogging loggerCustome, ApplicationDBContext dbContext, IMapper mapper, IVillaRepository villaRepository)
        {
            _logger = logger;
            _loggerCustome = loggerCustome;
            _dbContext = dbContext;
            _mapper = mapper;
            _villaRepository = villaRepository;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
           IEnumerable<Villa> villasList = await _villaRepository.GetAllAsync();
           return Ok(_mapper.Map<List<VillaDTO>>(villasList));
        }
        [HttpGet("{id:int}",Name =("GetVilla"))]
        // [ProducesResponseType(200,Type=typeof(VillaDTO))]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if(id == 0) {
                _logger.LogError("This is bad reguest");
                _loggerCustome.LogWithColor("This is bad reguest for custom", "error");
                return BadRequest();
            }
            var villa = await _villaRepository.GetAsync(x => x.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
             return Ok(_mapper.Map<VillaDTO>(villa));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreateDTO CreateDTOvilla)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _villaRepository.GetAsync(x => x.Name.ToLower() == CreateDTOvilla.Name.ToLower())!=null)
            {
                ModelState.AddModelError("CustomError", $"{CreateDTOvilla.Name} already Exists");
                return  BadRequest(ModelState);
            }
            if(CreateDTOvilla == null)
            {
                return BadRequest();
            }
            //if(villa.Id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}
            var model = _mapper.Map<Villa>(CreateDTOvilla);

           await _villaRepository.CreateAsync(model);
            return CreatedAtRoute("GetVilla", new { id= model.Id}, CreateDTOvilla);
           // return Ok(villa);
        }
        [HttpDelete("{id:int}",Name =("DeleteVilla"))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _villaRepository.GetAsync(u=>u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
           await _villaRepository.RemoveAsync(villa);
           
            return  NoContent();
        }
        [HttpPut("{id:int}", Name = ("UpdateVilla"))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]      
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO UpdateDTOvill)
        {
            if(UpdateDTOvill == null || id!= UpdateDTOvill.Id)
            {
                return BadRequest();
            }          
            var model = _mapper.Map<Villa>(UpdateDTOvill);           
            await _villaRepository.UpdateAsync(model);
           
            return NoContent();
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
