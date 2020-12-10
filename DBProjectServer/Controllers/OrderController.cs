using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using DBProject.ActionFilters;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Administrador")]
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

        [Authorize]
        [HttpGet("{id}", Name = "OrderById")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _repository.Order.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var orderResult = _mapper.Map<OrderDto>(order);

            return Ok(orderResult);
        }

        [HttpPost]
        [AllowAnonymous]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreationDto order)
        {
            var orderEntity = _mapper.Map<Order>(order);

            _repository.Order.CreateOrder(orderEntity);
            await _repository.SaveAsync();
            _repository.Order.GenerateOrderNumber(orderEntity);
            await _repository.SaveAsync();
            
            var createdOrder = _mapper.Map<OrderDto>(orderEntity);

            return CreatedAtRoute("OrderById", new { id = createdOrder.orderId }, createdOrder);
        }

        [Authorize]
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Order>))]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderForUpdateDto order)
        {
            var orderEntity = HttpContext.Items["entity"] as Order;

            _mapper.Map(order, orderEntity);

            _repository.Order.UpdateOrder(orderEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [Authorize]
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
