using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoSoftwareComision2.Controllers
{
    [Route("api/v1/dish")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly ICreateDishService createDish;
        private readonly IGetAllDishesService getAllDishes;
        private readonly IGetDishByIdService getDishById;
        private readonly IUpdateDishService updateDish;

        public DishesController(ICreateDishService createDish, IGetAllDishesService getAllDishes, IGetDishByIdService getDishById
            , IUpdateDishService updateDish)
        {
            this.createDish = createDish;
            this.getAllDishes = getAllDishes;
            this.getDishById = getDishById;
            this.updateDish = updateDish;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateDish([FromBody] CreateDishRequest request)
        {

            try
            {
                var dish = await createDish.CreateAsync(request);
                return Created(string.Empty, dish);
            }
            catch (DishNameAlreadyExistingException ex)
            {
                return Conflict(new ApiError { Message = ex.Message });
            }
            catch (InvalidPriceException ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
            catch (ExistingCategoryException ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
        }



        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DishResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery]string? name, [FromQuery] int? category,
            [FromQuery]string? sortByPrice, [FromQuery] bool? onlyActive)
        {
            try
            {
                var response = await getAllDishes.GetAllAsync(name, category, sortByPrice, onlyActive);
                return Ok(response);
            }
            catch (SortingParametersException ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
        }


        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dish = await getDishById.GetByIdAsync(id);
            if (dish is null) return NotFound();
            return Ok(dish);
        }


        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateDish(Guid id, [FromBody] DishUpdateRequest request)
        {
            try
            {
                var dish = await updateDish.UpdateAsync(id, request);
                return Ok(dish);
            }
            catch (DishNameAlreadyExistingException ex)
            {
                    return Conflict(new { message = ex.Message });
            }
            catch (DishNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidPriceException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }


    }
}
