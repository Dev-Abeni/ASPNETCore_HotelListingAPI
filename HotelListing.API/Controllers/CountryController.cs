using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.Dtos;
using HotelListing.API.IRepositroy;
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
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(
            IUnitOfWork unitOfWork, 
            ILogger<CountryController> logger, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet(Name = "GetCountries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await _unitOfWork.Countries.GetAll();

                var result = _mapper.Map<IList<CountryDisplayDto>>(countries);

                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCountries)}");
                
                return StatusCode(500);
            }
        }


        [HttpGet("{id:int}", Name = "GetCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                var country = await _unitOfWork.Countries.Get(c => c.Id == id, new List<string> { "Hotels" });

                var result = _mapper.Map<CountryDisplayDto>(country);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCountry)}");
                
                return StatusCode(500);
            }
        }


        [HttpPost(Name = "CreateCountry")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCountry([FromBody] CountryCreateDto countryCreateDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in { nameof(CreateCountry) }");

                return BadRequest(ModelState);
            }

            try
            {
                var country = _mapper.Map<Country>(countryCreateDto);
                await _unitOfWork.Countries.Insert(country);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetCountry", new { id = country.Id }, country);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the { nameof(CreateCountry) }");

                return StatusCode(500);
            }
        }


        [HttpPut("{id:int}", Name = "UpdateCountry")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] CountryUpdateDto countryUpdateDto)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in { nameof(UpdateCountry) }");
                return BadRequest(ModelState);
            }

            try
            {
                var country = await _unitOfWork.Countries.Get(c => c.Id == id);

                if (country == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in  { nameof(UpdateCountry) }");
                    return BadRequest("Country not found.");
                }

                _mapper.Map(countryUpdateDto, country);
                _unitOfWork.Countries.Update(country);

                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the { nameof(UpdateCountry) }");
                return StatusCode(500);
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteCountry")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in { nameof(DeleteCountry) }");
                return BadRequest();
            }

            try
            {
                var country = await _unitOfWork.Countries.Get(c => c.Id == id);
                if (country == null)
                {
                    _logger.LogError($"Invalid DELETE attempt in { nameof(DeleteCountry) }");
                    return BadRequest("Country not found.");
                }

                await _unitOfWork.Countries.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the { nameof(DeleteCountry) }");
                return StatusCode(500);
            }
        }
    }
}
