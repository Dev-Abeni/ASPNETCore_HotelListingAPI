using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<ApiUser> userManager, 
            IMapper mapper, 
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            _logger.LogInformation($"User registration attempted for : {userDto.Email}");

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var user = _mapper.Map<ApiUser>(userDto);
                user.UserName = userDto.Email;

                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(error.Code, error.Description);
                    
                    return BadRequest(ModelState);
                }

                return Ok(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Register)}");

                return StatusCode(500);
            }
        }

        //[HttpPost]
        //[Route("login")]
        //public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        //{
        //    _logger.LogInformation($"User login attempted for : {userLoginDto.EmailAddress}");

        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    try
        //    {
        //        var result = await _signInManager
        //            .PasswordSignInAsync(userLoginDto.EmailAddress, userLoginDto.Password, false, false);

        //        if (!result.Succeeded)
        //            return Unauthorized();

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Something went wrong in the {nameof(Login)}");

        //        return StatusCode(500);
        //    }
        //}
    }
}
