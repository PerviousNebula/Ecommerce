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
    public class CategoryController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public CategoryController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCategories([FromQuery] CategoryParameters categoryParameters)
        {
            var categories = await _repository.Category.GetAllCategoriesAsync(categoryParameters);

            var metadata = new
            {
                categories.TotalCount,
                categories.PageSize,
                categories.CurrentPage,
                categories.TotalPages,
                categories.HasNext,
                categories.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var categoriesResult = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return Ok(categoriesResult);
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "CategoryById")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Category>))]
        public IActionResult GetCategoryById(int id)
        {
            var category = HttpContext.Items["entity"] as Category;

            var categoryResult = _mapper.Map<CategoryDto>(category);

            return Ok(categoryResult);
        }

        [AllowAnonymous]
        [HttpGet("{id}/products")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Category>))]
        public async Task<IActionResult> GetCategoryWithProducts(int id)
        {
            var category = await _repository.Category.GetCustomerWithProductsAsync(id);

            var categoryResult = _mapper.Map<CategoryDto>(category);

            return Ok(categoryResult);
        }
    
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreationDto category)
        {
            var categoryEntity = _mapper.Map<Category>(category);

            _repository.Category.CreateCategory(categoryEntity);
            await _repository.SaveAsync();

            var createdCategory = _mapper.Map<CategoryDto>(categoryEntity);

            return CreatedAtRoute("CategoryById", new { id = createdCategory.categoryId }, createdCategory);
        }
    
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Category>))]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryForUpdateDto category)
        {
            var categoryEntity = HttpContext.Items["entity"] as Category;

            _mapper.Map(category, categoryEntity);

            _repository.Category.UpdateCategory(categoryEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Category>))]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryEntity = HttpContext.Items["entity"] as Category;

            _repository.Category.DeleteCategory(categoryEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
