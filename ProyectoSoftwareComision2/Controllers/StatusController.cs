using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoSoftwareComision2.Controllers
{
    //[Route("api/v1/[controller]")]
    //[ApiController]
    //public class StatusController : ControllerBase
    //{
    //    private readonly ICreateStatusService createStatus;
    //    private readonly IGetAllStatusService getAllStatus;

    //    public StatusController(ICreateStatusService createStatus,IGetAllStatusService getAllStatus)
    //    {
    //        this.createStatus = createStatus;
    //        this.getAllStatus = getAllStatus;
    //    }

    //    [HttpPost]
    //    [ProducesResponseType(typeof(StatusResponce), StatusCodes.Status201Created)]
    //    public async Task<IActionResult> CreateStatus([FromBody] CreateStatusRequest request)
    //    {
    //        var response = await createStatus.CreateAsync(request);
    //        return StatusCode(StatusCodes.Status201Created);
    //    }
    //    [HttpGet]
    //    [ProducesResponseType(typeof(IEnumerable<StatusResponce>), StatusCodes.Status200OK)]
    //    public async Task<IActionResult> GetAllStatus()
    //    {
    //        var response = await getAllStatus.GetAllAsync();
    //        return Ok(response);
    //    }
    //}
}
