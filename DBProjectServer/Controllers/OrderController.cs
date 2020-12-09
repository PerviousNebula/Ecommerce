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
    public class OrderController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public OrderController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery] OrderParameters orderParameters)
        {
            if (!orderParameters.ValidDateRange)
            {
                return BadRequest("Max year of creation cannot be less than min year of creation");
            }
            if (!orderParameters.ValidShippingRange)
            {
                return BadRequest("Max shipping total cannot be less than min shipping total of creation");
            }
            if (!orderParameters.ValidTotalRange)
            {
                return BadRequest("Max order total cannot be less than min order total");
            }

            var orders = await _repository.Order.GetAllOrdersAsync(orderParameters);

            var metadata = new
            {
                orders.TotalCount,
                orders.PageSize,
                orders.CurrentPage,
                orders.TotalPages,
                orders.HasNext,
                orders.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var ordersResult = _mapper.Map<IEnumerable<OrderDto>>(orders);

            return Ok(ordersResult);
        }

        [HttpGet("{id}", Name = "OrderById")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Order>))]
        public IActionResult GetOrderById(int id)
        {
            var order = HttpContext.Items["entity"] as Order;

            var orderResult = _mapper.Map<OrderDto>(order);

            return Ok(orderResult);
        }
    
        // [HttpPost]
        // [ServiceFilter(typeof(ValidationFilterAttribute))]
        // public async Task<IActionResult> CreateOrder([FromBody] List<SizeCreationDto> sizes)
        // {
        //     var sizesEntity = _mapper.Map<IEnumerable<Size>>(sizes);

        //     _repository.Size.CreateSizes(sizesEntity);
        //     await _repository.SaveAsync();

        //     return NoContent();
        // }
    
        // [HttpPut("{id}")]
        // [ServiceFilter(typeof(ValidationFilterAttribute))]
        // [ServiceFilter(typeof(ValidateEntityExistsAttribute<Size>))]
        // public async Task<IActionResult> UpdateSize(int id, [FromBody] SizeForUpdateDto size)
        // {
        //     var sizeEntity = HttpContext.Items["entity"] as Size;

        //     _mapper.Map(size, sizeEntity);

        //     _repository.Size.UpdateSize(sizeEntity);
        //     await _repository.SaveAsync();

        //     return NoContent();
        // }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Order>))]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var orderEntity = HttpContext.Items["entity"] as Order;

            _repository.Order.DeleteOrder(orderEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
