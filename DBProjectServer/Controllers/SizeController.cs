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
    public class SizeController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public SizeController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Product>))]
        public async Task<IActionResult> GetSizesByProductId(int id)
        {
            var sizes = await _repository.Size.GetSizesByProductId(id);

            var sizeResult = _mapper.Map<IEnumerable<SizeDto>>(sizes);

            return Ok(sizeResult);
        }
    
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateSizes([FromBody] List<SizeCreationDto> sizes)
        {
            var sizesEntity = _mapper.Map<IEnumerable<Size>>(sizes);

            _repository.Size.CreateSizes(sizesEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Size>))]
        public async Task<IActionResult> UpdateSize(int id, [FromBody] SizeForUpdateDto size)
        {
            var sizeEntity = HttpContext.Items["entity"] as Size;

            _mapper.Map(size, sizeEntity);

            _repository.Size.UpdateSize(sizeEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Size>))]
        public async Task<IActionResult> DeleteSize(int id)
        {
            var sizeEntity = HttpContext.Items["entity"] as Size;

            _repository.Size.DeleteSize(sizeEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
