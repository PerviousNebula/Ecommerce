using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using DBProject.ActionFilters;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public MenuController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Capturista")]
        public async Task<IActionResult> GetMenus(int id)
        {
            var menus = await _repository.Menu.GetMenus();

            var menusResult = _mapper.Map<IEnumerable<MenuDto>>(menus);

            return Ok(menusResult);
        }

        // [HttpPost]
        // [Authorize(Roles = "Administrador, Capturista")]
        // [ServiceFilter(typeof(ValidationFilterAttribute))]
        // public async Task<IActionResult> CreateColors([FromBody] List<ColorCreationDto> colors)
        // {
        //     var colorsEntity = _mapper.Map<IEnumerable<Color>>(colors);

        //     _repository.Color.CreateColors(colorsEntity);
        //     await _repository.SaveAsync();

        //     return NoContent();
        // }
    
        // [HttpPut("{id}")]
        // [Authorize(Roles = "Administrador, Capturista")]
        // [ServiceFilter(typeof(ValidationFilterAttribute))]
        // [ServiceFilter(typeof(ValidateEntityExistsAttribute<Color>))]
        // public async Task<IActionResult> UpdateColor(int id, [FromBody] ColorForUpdateDto color)
        // {
        //     var colorEntity = HttpContext.Items["entity"] as Color;

        //     _mapper.Map(color, colorEntity);

        //     _repository.Color.UpdateColor(colorEntity);
        //     await _repository.SaveAsync();

        //     return NoContent();
        // }

        // [HttpPut]
        // [Authorize(Roles = "Administrador, Capturista")]
        // [ServiceFilter(typeof(ValidationFilterAttribute))]
        // public async Task<IActionResult> UpdateColors([FromBody] List<ColorForUpdateDto> colors)
        // {
        //     var colorsEntity = _mapper.Map<IEnumerable<Color>>(colors);

        //     _repository.Color.UpdateColors(colorsEntity);
        //     await _repository.SaveAsync();

        //     return NoContent();
        // }
        
        // [HttpDelete("{id}")]
        // [Authorize(Roles = "Administrador, Capturista")]
        // [ServiceFilter(typeof(ValidateEntityExistsAttribute<Color>))]
        // public async Task<IActionResult> DeleteColor(int id)
        // {
        //     var colorEntity = HttpContext.Items["entity"] as Color;

        //     _repository.Color.DeleteColor(colorEntity);
        //     await _repository.SaveAsync();

        //     return NoContent();
        // }
    }
}
