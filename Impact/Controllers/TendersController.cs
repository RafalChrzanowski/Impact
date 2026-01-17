using Impact.Models;
using Impact.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TendersController : ControllerBase
{
    private readonly ITendersService _service;

    public TendersController(ITendersService service)
    {
        _service = service;
    }

    [HttpGet("tenders")]
    public async Task<ActionResult<PagedResponse<TenderDto>>> GetTenders([FromQuery] TenderFilterDto filter)
    {
        var result = await _service.GetTendersAsync(filter);
        return Ok(result);
    }
}
