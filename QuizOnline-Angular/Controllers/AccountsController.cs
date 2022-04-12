using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Quiz.Application.Implementations;
using Quiz.Application.Interfaces;
using Quiz.Data.Models;
using Quiz.Data.Models.Dtos;
using Quiz.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Quiz.Controllers
{
    [Authorize]
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly JWTService _jWTService;
      

        private readonly IMapper _mapper;
        public AccountsController( IMapper mapper, IConfiguration config, JWTService jWTService)
        {
            _config = config;
            _jWTService = jWTService;
            _mapper = mapper;
        }
        

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

            var user = _mapper.Map<User>(userForRegistration);

            var result = _jWTService.AddUser(user);
            if (!result.IsSuccessfulRegistration)
            {
                return BadRequest();
            }
        
            return StatusCode(201);
        }
    }
}
