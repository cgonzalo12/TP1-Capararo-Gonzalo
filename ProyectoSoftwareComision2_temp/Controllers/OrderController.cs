using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace ProyectoSoftwareComision2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IGetAllOrdersService getAllOrders;
        private readonly ICreateOrderService createOrder;
        private readonly IGetOrderByIdService getOrderBy;
        private readonly IUpdateOrderService updateOrderService;
        private readonly IOrderItemUpdateService itemUpdateService;

        public OrderController(IGetAllOrdersService getAllOrders, ICreateOrderService createOrder,
            IGetOrderByIdService getOrderBy,IUpdateOrderService updateOrderService,
            IOrderItemUpdateService itemUpdateService)
        {
            this.getAllOrders = getAllOrders;
            this.createOrder = createOrder;
            this.getOrderBy = getOrderBy;
            this.updateOrderService = updateOrderService;
            this.itemUpdateService = itemUpdateService;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderDetailsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync([FromQuery] long? statusId, [FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFin)
        {
            try
            {
                var orders = await getAllOrders.GetAllAsync(statusId, fechaInicio, fechaFin);
                return Ok(orders);
            }
            catch (DateRangeException ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }

        }
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            try
            {
                var order = await getOrderBy.GetByIdAsync(id);
                if (order == null) return NotFound();
                return Ok(order);
            }
            catch (ActiveDishException ex)
            {
                return NotFound(new ApiError { Message = ex.Message });
            }
            catch(OrderNotFundException ex)
            {
                return NotFound(new ApiError { Message = ex.Message });
            }
            

        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderDetailsResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                var response = await createOrder.CreateAsync(request);
                return Created(string.Empty!,response);
            }
            catch (ActiveDishException ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });

            }
            catch (QuantityException ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
            catch (DeliverytypeException ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }

        }
        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOrderAsync(long id,OrderUpdateRequest updateRequest)
        {
            try
            {
                var order = await updateOrderService.UpdateAsync(id, updateRequest);
                return Ok(order);
            }
            catch(DishNotAvailableException ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
            catch (ClosedOrderException ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
            catch(DishNotFoundException ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
            

        }
        [HttpPut("{id:long}/item/{itemId:guid}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]//algo
        public async Task<IActionResult> UpdateOrderItemAsync(long id, Guid itemId, OrderItemUpdateRequest itemUpdateRequest)
        {
            try
            {
                var order = await itemUpdateService.UpdateOrderItemsAsync(id, itemId, itemUpdateRequest);
                return Ok(order);
            }
            catch (InvalidStatusException ex)
            {
                return NotFound(new ApiError { Message = ex.Message });
            }
            catch (OrderNotFundException ex)
            {
                return NotFound(new ApiError { Message = ex.Message });
            }
            catch (ClosedOrderException ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
            catch (ItemNotFundInTheOrderException ex)
            {
                return NotFound(new ApiError { Message = ex.Message });
            }
            catch (InvalidTransactionException ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }

        }

    }
}
