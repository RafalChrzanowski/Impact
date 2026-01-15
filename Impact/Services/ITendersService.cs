using Impact.Models;

namespace Impact.Services
{
    public interface ITendersService
    {
        Task<List<TenderDto>> GetAllTendersAsync();
    }
}