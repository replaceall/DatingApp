using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.DTO;
using DatingApp.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repo , IConfiguration config, IMapper mapper)
        {
            _repo = repo;
            _config = config;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO user)
        {
            user.UserName = user.UserName.ToLower();
            if(await _repo.UserExists(user.UserName))
            {
                return BadRequest("Username already exists");
            }
            var userToCreate = new UserModel
            {
                UserName = user.UserName
            };
            var createdUser = await _repo.Register(userToCreate, user.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLogin)
        {
            //try
            //{
               // throw new Exception("Computer says no");
                var userFromRepo = await _repo.LogIn(userForLogin.UserName, userForLogin.Password);
                if (userFromRepo == null)
                {
                    return Unauthorized();
                }
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)

            };
                var key = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(_config.GetSection("AppSettings:Token").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDesripter = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDesripter);
                var user = _mapper.Map<UserForListDto>(userFromRepo);

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    user
                });
            //} catch(Exception ex)
            //{
            //    return StatusCode(500, "Computer Says No");
            //}
           
        }


    }
}
