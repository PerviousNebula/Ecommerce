using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using DBProject.ActionFilters;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DBProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private readonly IConfiguration _config;

        public UserController(IRepositoryWrapper repository, IMapper mapper, IConfiguration config)
        {
            _repository = repository;
            _mapper = mapper;
            _config = config;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _repository.User.GetUsers();

            var usersResult = _mapper.Map<IEnumerable<UserDto>>(users);

            return Ok(usersResult);
        }

        [HttpGet("{id}", Name = "UserById")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<User>))]
        [Authorize]
        public IActionResult GetUserById(int id)
        {
            var user = HttpContext.Items["entity"] as User;

            var userResult = _mapper.Map<UserDto>(user);

            return Ok(userResult);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreationDto user)
        {
            var userEntity = _mapper.Map<User>(user);

            CreatePasswordHash(user.password, out byte[] passwordHash, out byte[] passwordSalt);
            userEntity.passwordHash = passwordHash;
            userEntity.passwordSalt = passwordSalt;

            _repository.User.CreateUser(userEntity);
            await _repository.SaveAsync();

            var createdUser = _mapper.Map<UserDto>(userEntity);

            return CreatedAtRoute("UserById", new { id = createdUser.userId }, createdUser);
        }
    
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<User>))]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserForUpdateDto user)
        {
            var userEntity = HttpContext.Items["entity"] as User;

            if (!string.IsNullOrEmpty(user.password) || user.password != null)
            {
                CreatePasswordHash(user.password, out byte[] passwordHash, out byte[] passwordSalt);
                user.passwordHash = passwordHash;
                user.passwordSalt = passwordSalt;
            }
            else
            {
                user.passwordHash = userEntity.passwordHash;
                user.passwordSalt = userEntity.passwordSalt;
            }

            _mapper.Map(user, userEntity);

            _repository.User.UpdateUser(userEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<User>))]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userEntity = HttpContext.Items["entity"] as User;

            _repository.User.DeleteUser(userEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {
            var userEntity = await _repository.User.GetUserByEmail(user.email);
            if (userEntity == null)
            {
                return NotFound("User not found");
            }

            if (!VerifyPasswordHash(user.password, userEntity.passwordHash, userEntity.passwordSalt))
            {
                return NotFound("Invalid email or password");
            }

            var claims  = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userEntity.id.ToString()),
                new Claim(ClaimTypes.Email, userEntity.email),
                new Claim(ClaimTypes.Name, userEntity.name),
                new Claim(ClaimTypes.Role, userEntity.rolId == 1 ? "Administrador" : "Capturista")
            };

            var userResult = _mapper.Map<UserDto>(userEntity);

            return Ok(new {
                user = userResult,
                token = TokenGeneration(claims)
            });
        }
    
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var newPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passwordHash).SequenceEqual(new ReadOnlySpan<byte>(newPasswordHash));
            }
        }

        private string TokenGeneration(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:ClaveSecreta"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: creds,
              claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
