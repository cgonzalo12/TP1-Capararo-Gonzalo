using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoSoftwareComision2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICreateCategoryService createCategory;
        private readonly IGetAllCategoryService getAllCategory;

        //constructor
        public CategoryController(ICreateCategoryService createCategory,IGetAllCategoryService getAllCategory)
        {
            this.createCategory = createCategory;
            this.getAllCategory = getAllCategory;
        }

        // 
        [HttpPost]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            var response = await createCategory.CreateAsync(request);
            return StatusCode(StatusCodes.Status201Created);

        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await getAllCategory.GetAllAsync();
            return Ok(response);
        }
    }
}
