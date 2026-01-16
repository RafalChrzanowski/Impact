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

    [HttpGet("filterByAmount")]
    public async Task<IEnumerable<TenderDto>> GetTendersByAmount(
    [FromQuery] decimal? minAmount = null,
    [FromQuery] decimal? maxAmount = null)
    {
        return await _service.GetTendersByAmountAsync(minAmount, maxAmount);
    }

    [HttpGet("orderByAmount")]
    public async Task<IEnumerable<TenderDto>> GetTendersOrderedByAmount([FromQuery] bool asc = true)
    {
        return await _service.GetTendersOrderedByAmountAsync(asc);
    }

    [HttpGet("filterByDate")]
    public async Task<IEnumerable<TenderDto>> GetTendersByDate(
    [FromQuery] DateTime? startDate = null,
    [FromQuery] DateTime? endDate = null)
    {
        return await _service.GetTendersByDateAsync(startDate, endDate);
    }


    [HttpGet("orderByDate")]
    public async Task<IEnumerable<TenderDto>> GetTendersOrderedByDate([FromQuery] bool asc = true)
    {
        return await _service.GetTendersOrderedByDateAsync(asc);
    }

    [HttpGet("filterBySupplierId")]
    public async Task<IEnumerable<TenderDto>> GetTendersBySupplierId(
    [FromQuery] int? supplierId = null)
    {
        return await _service.GetTendersBySupplierIdAsync(supplierId);
    }

    [HttpGet("filterByTenderId")]
    public async Task<IEnumerable<TenderDto>> GetTendersByTenderId(
    [FromQuery] int? tenderId = null)
    {
        return await _service.GetTendersByTenderIdAsync(tenderId);
    }
}
