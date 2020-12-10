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
    public class OrderDetailController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public OrderDetailController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("{orderId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateOrderDetail(int orderId, [FromBody] List<OrderDetailCreationDto> orderDetails)
        {
            var orderTotal = 0.0;
            foreach (var od in orderDetails)
            {
                var orderDetailEntity = _mapper.Map<OrderDetail>(od);

                orderDetailEntity.total = await CalculateOrderDetailPrice(orderDetailEntity.ProductDesign, orderDetailEntity.quantity);
                _repository.OrderDetail.CreateOrderDetail(orderDetailEntity);

                orderTotal += orderDetailEntity.total;
            }

            var order = await _repository.Order.GetOrderByIdAsync(orderId);
            foreach (var od in order.OrderDetails)
            {
                var productDesign = await _repository.ProductDesign.GetProductDesignByIdAsync(od.productDesignId);
                var detailTotal = await CalculateOrderDetailPrice(productDesign, od.quantity);
                orderTotal += detailTotal;
            }
            order.total = orderTotal;
            _repository.Order.UpdateOrder(order);

            await _repository.SaveAsync();

            return NoContent();
        }
    
        [HttpPut("{orderId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateOrderDetail(int orderId, [FromBody] List<OrderDetailForUpdateDto> orderDetails)
        {
            var orderTotal = 0.0;
            foreach (var od in orderDetails)
            {
                var orderDetailEntity = await _repository.OrderDetail.GetOrderDetailByIdAsync(od.orderDetailId);
                if (orderDetailEntity == null)
                {
                    return NotFound();
                }
                _mapper.Map(od, orderDetailEntity);

                orderDetailEntity.total = await CalculateOrderDetailPrice(orderDetailEntity.ProductDesign, orderDetailEntity.quantity);
                _repository.OrderDetail.UpdateOrderDetail(orderDetailEntity);

                orderTotal += orderDetailEntity.total;
            }
            var order = await _repository.Order.GetOrderByIdAsync(orderId);
            order.total = orderTotal;
            _repository.Order.UpdateOrder(order);

            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("order/{orderId}/productDesign/{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<ProductDesign>))]
        public async Task<IActionResult> DeleteOrderDetail(int orderId, int id)
        {
            var productDesignEntity = HttpContext.Items["entity"] as ProductDesign;
            _repository.ProductDesign.DeleteProductDesign(productDesignEntity);
            await _repository.SaveAsync();

            var order = await _repository.Order.GetOrderByIdAsync(orderId);
            var orderTotal = 0.0;
            foreach (var od in order.OrderDetails)
            {
                var productDesign = await _repository.ProductDesign.GetProductDesignByIdAsync(od.productDesignId);
                var detailTotal = await CalculateOrderDetailPrice(productDesign, od.quantity);
                orderTotal += detailTotal;
            }
            order.total = orderTotal;
            _repository.Order.UpdateOrder(order);

            await _repository.SaveAsync();

            return NoContent();
        }

        public async Task<double> CalculateOrderDetailPrice(ProductDesign productDesign, int quantity)
        {
            var productPrice = (await _repository.Product.GetProductByIdAsync(productDesign.productId)).price;
            var sizeExtraPrice = (await _repository.Size.GetSizeById(productDesign.sizeId)).priceIncrement;
            var colorExtraPrice = (await _repository.Color.GetColorByIdAsync(productDesign.colorId)).priceIncrement;
            return (productPrice + sizeExtraPrice + colorExtraPrice) * quantity;
        }
    }
}
