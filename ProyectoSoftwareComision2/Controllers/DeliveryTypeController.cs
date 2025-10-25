using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoSoftwareComision2.Controllers
{
    //[Route("api/v1/[controller]")]
    //[ApiController]
    //public class DeliveryTypeController : ControllerBase
    //{
    //    private readonly ICreateDeliveryTypeService createDeliveryType;
    //    private readonly IGetAllDeliveryTypeService getAllDeliveryType;

    //    public DeliveryTypeController(ICreateDeliveryTypeService createDeliveryType,IGetAllDeliveryTypeService getAllDeliveryType)
    //    {
    //        this.createDeliveryType = createDeliveryType;
    //        this.getAllDeliveryType = getAllDeliveryType;
    //    }

    //    [HttpPost]
    //    [ProducesResponseType(typeof(DeliveryTypeResponce), StatusCodes.Status201Created)]
    //    public async Task<IActionResult> CreateDeliveryType([FromBody] CreateDeliveryTypeRequest request)
    //    {
    //        var response = await createDeliveryType.CreateAsync(request);
    //        return StatusCode(StatusCodes.Status201Created);
    //    }

    //    [HttpGet]
    //    [ProducesResponseType(typeof(IEnumerable<DeliveryTypeResponce>), StatusCodes.Status200OK)]
    //    public async Task<IActionResult> GetAllDeliveryType()
    //    {
    //        var response = await getAllDeliveryType.GetAllAsync();
    //        return Ok(response);
    //    }
    //}
}
