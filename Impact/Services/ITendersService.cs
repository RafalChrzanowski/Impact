using Impact.Models;

public interface ITendersService
{
    Task<PagedResponse<TenderDto>> GetTendersAsync(TenderFilterDto filter);
}
