using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public RolController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _repository.Rol.GetRoles();

            var rolesResult = _mapper.Map<IEnumerable<RolDto>>(roles);

            return Ok(rolesResult);
        }
    }
}
