using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using DBProject.ActionFilters;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColorController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public ColorController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Product>))]
        public async Task<IActionResult> GetColorsByProductId(int id)
        {
            var colors = await _repository.Color.GetColorsByProductId(id);

            var colorResult = _mapper.Map<IEnumerable<ColorDto>>(colors);

            return Ok(colorResult);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateColors([FromBody] List<ColorCreationDto> colors)
        {
            var colorsEntity = _mapper.Map<IEnumerable<Color>>(colors);

            _repository.Color.CreateColors(colorsEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Color>))]
        public async Task<IActionResult> UpdateColor(int id, [FromBody] ColorForUpdateDto color)
        {
            var colorEntity = HttpContext.Items["entity"] as Color;

            _mapper.Map(color, colorEntity);

            _repository.Color.UpdateColor(colorEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Color>))]
        public async Task<IActionResult> DeleteColor(int id)
        {
            var colorEntity = HttpContext.Items["entity"] as Color;

            _repository.Color.DeleteColor(colorEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
