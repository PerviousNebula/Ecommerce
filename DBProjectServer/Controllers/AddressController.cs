using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using DBProject.ActionFilters;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DBProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public AddressController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAddresses([FromQuery] AddressParameters addressParameters)
        {
            var addresses = await _repository.Address.GetAllAddressesAsync(addressParameters);

            var metadata = new
            {
                addresses.TotalCount,
                addresses.PageSize,
                addresses.CurrentPage,
                addresses.TotalPages,
                addresses.HasNext,
                addresses.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var addressessResult = _mapper.Map<IEnumerable<AddressDto>>(addresses);

            return Ok(addressessResult);
        }

        [HttpGet("{id}", Name = "AddressById")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Address>))]
        public IActionResult GetAddressById(int id)
        {
            var address = HttpContext.Items["entity"] as Address;

            var addressResult = _mapper.Map<AddressDto>(address);

            return Ok(addressResult);
        }
    
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateAddress([FromBody] AddressCreationDto address)
        {
            var addressEntity = _mapper.Map<Address>(address);

            _repository.Address.CreateAddress(addressEntity);
            await _repository.SaveAsync();

            var createdAddress = _mapper.Map<AddressDto>(addressEntity);

            return CreatedAtRoute("AddressById", new { id = createdAddress.addressId }, createdAddress);
        }
    
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Address>))]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressForUpdateDto address)
        {
            var addressEntity = HttpContext.Items["entity"] as Address;

            _mapper.Map(address, addressEntity);

            _repository.Address.UpdateAddress(addressEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Address>))]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var addressEntity = HttpContext.Items["entity"] as Address;

            _repository.Address.DeleteAddress(addressEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
