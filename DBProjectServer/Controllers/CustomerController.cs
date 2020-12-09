using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using DBProject.ActionFilters;
using DBProject.Extensions;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DBProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private IConfiguration _config;

        public CustomerController(IRepositoryWrapper repository, IMapper mapper, IConfiguration config)
        {
            _repository = repository;
            _mapper = mapper;
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers([FromQuery] CustomerParameters customerParameters)
        {
            if (!customerParameters.ValidYearRange)
            {
                return BadRequest("Max year of signup cannot be less than min year of signup");
            }

            var customers = await _repository.Customer.GetAllCustomersAsync(customerParameters);

            var metadata = new
            {
                customers.TotalCount,
                customers.PageSize,
                customers.CurrentPage,
                customers.TotalPages,
                customers.HasNext,
                customers.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var customersResult = _mapper.Map<IEnumerable<CustomerDto>>(customers);

            return Ok(customersResult);
        }

        [HttpGet("{id}", Name = "CustomerById")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Customer>))]
        public IActionResult GetCustomerById(int id)
        {
            var customer = HttpContext.Items["entity"] as Customer;

            var customerResult = _mapper.Map<CustomerDto>(customer);

            return Ok(customerResult);
        }

        [HttpGet("{id}/address")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Customer>))]
        public async Task<IActionResult> GetCustomerWithDetails(int id)
        {
            var customer = await _repository.Customer.GetCustomerWithAddressesAsync(id);

            var customerResult = _mapper.Map<CustomerDto>(customer);

            return Ok(customerResult);
        }
    
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreationDto customer)
        {
            var customerEntity = _mapper.Map<Customer>(customer);

            AuthExtensions.CreatePasswordHash(customer.password, out byte[] passwordHash, out byte[] passwordSalt);
            customer.passwordHash = passwordHash;
            customer.passwordSalt = passwordSalt;

            _repository.Customer.CreateCustomer(customerEntity);
            await _repository.SaveAsync();

            var createdCustomer = _mapper.Map<CustomerDto>(customerEntity);

            return CreatedAtRoute("CustomerById", new { id = createdCustomer.customerId }, createdCustomer);
        }
    
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Customer>))]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerForUpdateDto customer)
        {
            var customerEntity = HttpContext.Items["entity"] as Customer;

            if (!string.IsNullOrEmpty(customer.password))
            {
                AuthExtensions.CreatePasswordHash(customer.password, out byte[] passwordHash, out byte[] passwordSalt);
                customer.passwordHash = passwordHash;
                customer.passwordSalt = passwordSalt;
            }
            else
            {
                customer.passwordHash = customerEntity.passwordHash;
                customer.passwordSalt = customerEntity.passwordSalt;
            }

            _mapper.Map(customer, customerEntity);

            _repository.Customer.UpdateCustomer(customerEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Customer>))]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customerEntity = HttpContext.Items["entity"] as Customer;

            _repository.Customer.DeleteCustomer(customerEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    
        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Login([FromBody] LoginDto customer)
        {
            var customerEntity = await _repository.Customer.GetCustomerByEmail(customer.email);
            if (customerEntity == null)
            {
                return NotFound();
            }

            if (!AuthExtensions.VerifyPasswordHash(customer.password, customerEntity.passwordHash, customerEntity.passwordSalt))
            {
                return NotFound("Invalid email or password");
            }

            var claims  = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, customerEntity.id.ToString()),
                new Claim(ClaimTypes.Email, customerEntity.email),
                new Claim(ClaimTypes.Name, customerEntity.name)
            };

            var customerResult = _mapper.Map<CustomerDto>(customerEntity);

            return Ok(new {
                customer = customerResult,
                token = AuthExtensions.TokenGeneration(claims, _config)
            });
        }
    }
}
