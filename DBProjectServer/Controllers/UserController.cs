using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using DBProject.ActionFilters;
using DBProject.Extensions;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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

        [Authorize]
        [HttpGet("{id}", Name = "UserById")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<User>))]
        public IActionResult GetUserById(int id)
        {
            var user = HttpContext.Items["entity"] as User;

            var userResult = _mapper.Map<UserDto>(user);

            return Ok(userResult);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateUser([FromBody] UserCreationDto user)
        {
            var userEntity = _mapper.Map<User>(user);

            AuthExtensions.CreatePasswordHash(user.password, out byte[] passwordHash, out byte[] passwordSalt);
            userEntity.passwordHash = passwordHash;
            userEntity.passwordSalt = passwordSalt;

            _repository.User.CreateUser(userEntity);
            await _repository.SaveAsync();

            var createdUser = _mapper.Map<UserDto>(userEntity);

            return CreatedAtRoute("UserById", new { id = createdUser.userId }, createdUser);
        }
    
        [Authorize]
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<User>))]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserForUpdateDto user)
        {
            var userEntity = HttpContext.Items["entity"] as User;

            if (!string.IsNullOrEmpty(user.password) || user.password != null)
            {
                AuthExtensions.CreatePasswordHash(user.password, out byte[] passwordHash, out byte[] passwordSalt);
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
        [Authorize(Roles = "Administrador")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<User>))]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userEntity = HttpContext.Items["entity"] as User;

            _repository.User.DeleteUser(userEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {
            var userEntity = await _repository.User.GetUserByEmail(user.email);
            if (userEntity == null)
            {
                return NotFound();
            }

            if (!AuthExtensions.VerifyPasswordHash(user.password, userEntity.passwordHash, userEntity.passwordSalt))
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
                token = AuthExtensions.TokenGeneration(claims, _config)
            });
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult RefreshToken([FromBody] TokenDto model)
        {
            var validatedClaims = (AuthExtensions.VerifyToken(model.token, _config));
            if (validatedClaims == null)
            {
                return Unauthorized();
            }

            var user = new UserDto
            {
                userId = int.Parse(validatedClaims.First(c => c.Type == ClaimTypes.NameIdentifier).Value),
                email = validatedClaims.First(c => c.Type == ClaimTypes.Email).Value,
                name = validatedClaims.First(c => c.Type == ClaimTypes.Name).Value
            };

            return Ok(new {
                user,
                token = AuthExtensions.TokenGeneration(validatedClaims.ToList(), _config)
            });
        }
    }
}
