using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Notes.Data;
using Notes.Dtos;
using Notes.Helpers;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Notes.Controllers
{
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUsersRepo _repository;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("Users/Register")]
        public ActionResult<UserReadDto> CreateUser(UserCreateDto userCreateDto)
        {
            if (userCreateDto == null)
            {
                return NotFound();
            }

            var existedUser = _repository.GetUserByUsername(userCreateDto.Username);
            if (existedUser != null)
            {
                return StatusCode(500, new { error = "User with this username already exist" });
            }

            if (userCreateDto.Password.Length < 8)
            {
                return StatusCode(500, new { error = "Password should contain 8 and more digits" });
            }

            UserCreateDtoHashed user = new UserCreateDtoHashed 
            {
                Username = userCreateDto.Username,
                Password = Helpers.Password.GetHashPassword(userCreateDto.Password) 
            };

            var userModel = _mapper.Map<User>(user);

            _repository.CreateUser(userModel);
            _repository.SaveChanges();

            var userReadDto = _mapper.Map<UserReadDto>(userModel);

            return Ok(userReadDto);
        }

        [HttpPost("Users/Login")]
        public ActionResult<UserReadDto> Login(UserCreateDto userCreateDto)
        {
            UserCreateDtoHashed userCreateDtoHashed = new UserCreateDtoHashed
            {
                Username = userCreateDto.Username,
                Password = Helpers.Password.GetHashPassword(userCreateDto.Password)
            };

            var userModel = _mapper.Map<User>(userCreateDtoHashed);

            var user = _repository.Login(userModel);

            if (user == null)
            {
                return StatusCode(500);
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                    new Claim(ClaimTypes.NameIdentifier as string, user.Id.ToString())
                };

            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimTypes.NameIdentifier);

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: claimsIdentity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = user.Username,
                id = user.Id,
            };

            return Json(response);
            
        }
    }
}
