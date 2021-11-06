using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.Dtos;
using HotelListing.API.IRepositroy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HotelController> _logger;
        private readonly IMapper _mapper;

        public HotelController(
            IUnitOfWork unitOfWork, 
            ILogger<HotelController> logger, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet(Name = "GetHotels")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotels()
        {
            try
            {
                var hotels = await _unitOfWork.Hotels.GetAll();

                var result = _mapper.Map<IList<HotelDisplayDto>>(hotels);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetHotels)}");

                return StatusCode(500);
            }
        }

        
        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotel(int id)
        {
            try
            {
                var hotel = await _unitOfWork.Hotels.Get(h => h.Id == id, new List<string> { "Country" });

                var result = _mapper.Map<HotelDisplayDto>(hotel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetHotel)}");

                return StatusCode(500);
            }
        }


        [HttpPost(Name = "CreateHotel")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateHotel([FromBody] HotelFormDto hotelFormDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in { nameof(CreateHotel) }");

                return BadRequest(ModelState);
            }

            try
            {
                var hotel = _mapper.Map<Hotel>(hotelFormDto);
                await _unitOfWork.Hotels.Insert(hotel);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetHotel", new { id = hotel.Id }, hotel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the { nameof(CreateHotel) }");

                return StatusCode(500);
            }
        }


        [HttpPut("{id:int}", Name = "UpdateHotel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] HotelFormDto hotelFormDto)
        {
            if(!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in { nameof(UpdateHotel) }");
                return BadRequest(ModelState);
            }

            try
            {
                var hotel = await _unitOfWork.Hotels.Get(h => h.Id == id);

                if(hotel == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in  { nameof(UpdateHotel) }");
                    return BadRequest("Hotel not found.");
                }

                _mapper.Map(hotelFormDto, hotel);
                _unitOfWork.Hotels.Update(hotel);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the { nameof(UpdateHotel) }");
                return StatusCode(500);
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteHotel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in { nameof(DeleteHotel) }");
                return BadRequest();
            }

            try
            {
                var hotel = await _unitOfWork.Hotels.Get(h => h.Id == id);
                if(hotel == null)
                {
                    _logger.LogError($"Invalid DELETE attempt in { nameof(DeleteHotel) }");
                    return BadRequest("Hotel not found.");
                }

                await _unitOfWork.Hotels.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the { nameof(DeleteHotel) }");
                return StatusCode(500);
            }
        }
    }
}
