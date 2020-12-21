using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using DBProject.ActionFilters;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

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
        [Authorize(Roles = "Administrador")]
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

        [Authorize]
        [HttpGet("{id}", Name = "AddressById")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Address>))]
        public IActionResult GetAddressById(int id)
        {
            var address = HttpContext.Items["entity"] as Address;

            var addressResult = _mapper.Map<AddressDto>(address);

            return Ok(addressResult);
        }
    
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateAddress([FromBody] AddressCreationDto address)
        {
            var addressEntity = _mapper.Map<Address>(address);

            _repository.Address.CreateAddress(addressEntity);
            await _repository.SaveAsync();

            var createdAddress = _mapper.Map<AddressDto>(addressEntity);

            return CreatedAtRoute("AddressById", new { id = createdAddress.addressId }, createdAddress);
        }

        [Authorize]
        [HttpPost("list")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateAddresses([FromBody] List<AddressCreationDto> addresses)
        {
            var addressesEntity = _mapper.Map<IEnumerable<Address>>(addresses);

            _repository.Address.CreateAddresses(addressesEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    
        [Authorize]
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

        [Authorize]
        [HttpPut("list")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateAddresses([FromBody] List<AddressForUpdateDto> addresses)
        {
            var addressesEntity = _mapper.Map<IEnumerable<Address>>(addresses);

            _repository.Address.UpdateAddresses(addressesEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Address>))]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var addressEntity = HttpContext.Items["entity"] as Address;

            _repository.Address.DeleteAddress(addressEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("countries")]
        public async Task<IActionResult> GetCountries()
        {
            var client = new RestClient("https://restcountries.eu");
            var request = new RestRequest("/rest/v2/all", Method.GET, DataFormat.Json);
            var countries = await client.GetAsync<IEnumerable<CountryDto>>(request);
            return Ok(countries);
        }
    }
}
