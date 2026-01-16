using Impact.Models;

namespace Impact.Services
{
    public interface ITendersService
    {
        Task<List<TenderDto>> GetAllTendersAsync();
        Task<IEnumerable<TenderDto>> GetTendersByAmountAsync(decimal? minAmount = null, decimal? maxAmount = null);
        Task<IEnumerable<TenderDto>> GetTendersOrderedByAmountAsync(bool asc);
        Task<IEnumerable<TenderDto>> GetTendersByDateAsync(DateTime? startDate, DateTime? endDate);
        Task<IEnumerable<TenderDto>> GetTendersOrderedByDateAsync(bool asc);
        Task<IEnumerable<TenderDto>> GetTendersBySupplierIdAsync(int? supplierId);
        Task<IEnumerable<TenderDto>> GetTendersByTenderIdAsync(int? tenderId);
    }
}