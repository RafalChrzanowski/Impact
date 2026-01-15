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

    [HttpGet]
    public async Task<IEnumerable<TenderDto>> GetTenders()
    {
        return await _service.GetAllTendersAsync();
    }
}
