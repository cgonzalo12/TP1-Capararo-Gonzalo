using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoSoftwareComision2.Controllers
{
    //[Route("api/v1/[controller]")]
    //[ApiController]
    //public class OrderItemController : ControllerBase
    //{
    //    private readonly ICreateOrderItemService createOrderItem;
    //    private readonly IGetAllOrderItemService getAllOrderItem;

    //    public OrderItemController(ICreateOrderItemService createOrderItem,IGetAllOrderItemService getAllOrderItem)
    //    {
    //        this.createOrderItem = createOrderItem;
    //        this.getAllOrderItem = getAllOrderItem;
    //    }
    //    [HttpPost]
    //    [ProducesResponseType(typeof(OrderItemResponse), StatusCodes.Status201Created)]
    //    public async Task<IActionResult> CreateOrderItem([FromBody] Items request)
    //    {
    //        var response = await createOrderItem.CreateAsync(request);
    //        return StatusCode(StatusCodes.Status201Created);
    //    }
    //}
}
