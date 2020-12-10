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
    public class ProductController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public ProductController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductParameters productParameters)
        {
            if (!productParameters.ValidaPriceRange)
            {
                return BadRequest("Max price value of product cannot be less than min price of product");
            }

            var products = await _repository.Product.GetAllProductsAsync(productParameters);

            var metadata = new
            {
                products.TotalCount,
                products.PageSize,
                products.CurrentPage,
                products.TotalPages,
                products.HasNext,
                products.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var productsResult = _mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(productsResult);
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "ProductById")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Product>))]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _repository.Product.GetProductByIdAsync(id);

            var productResult = _mapper.Map<ProductDto>(product);

            return Ok(productResult);
        }
    
        [HttpPost]
        [Authorize(Roles = "Administrador, Capturista")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreationDto product)
        {
            var productEntity = _mapper.Map<Product>(product);

            _repository.Product.CreateProduct(productEntity);
            await _repository.SaveAsync();

            var createdProduct = _mapper.Map<ProductDto>(productEntity);

            return CreatedAtRoute("ProductById", new { id = createdProduct.productId }, createdProduct);
        }
    
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador, Capturista")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Product>))]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductForUpdateDto product)
        {
            var productEntity = HttpContext.Items["entity"] as Product;

            _mapper.Map(product, productEntity);

            _repository.Product.UpdateProduct(productEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador, Capturista")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Product>))]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productEntity = HttpContext.Items["entity"] as Product;

            _repository.Product.DeleteProduct(productEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
