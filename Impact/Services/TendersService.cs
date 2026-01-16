using Impact.Mappers;
using Impact.Models;
using System.Net.Http.Json;

namespace Impact.Services
{
    public class TendersService : ITendersService
    {
        private const string BaseUrl = "https://tenders.guru/api/pl/tenders";
        private readonly HttpClient _httpClient;

        private List<TenderDto>? _tendersCache;

        public TendersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TenderDto>> GetAllTendersAsync()
        {
            if (_tendersCache != null)
                return _tendersCache;

            _tendersCache = new List<TenderDto>();
            for (int page = 1; page <= 100; page++)
            {
                var response = await _httpClient.GetFromJsonAsync<TenderResponse>($"{BaseUrl}?page={page}");
                if (response?.Data == null || !response.Data.Any())
                    break;

                _tendersCache.AddRange(response.Data.Select(TenderMapper.ToDto));
            }

            return _tendersCache;
        }

        public async Task<IEnumerable<TenderDto>> GetTendersByAmountAsync(decimal? min, decimal? max)
        {
            if (_tendersCache == null)
                await GetAllTendersAsync();

            var query = _tendersCache.AsEnumerable();

            if (min.HasValue)
                query = query.Where(t => t.AmountEur >= min.Value);

            if (max.HasValue)
                query = query.Where(t => t.AmountEur <= max.Value);

            return query;
        }

        public async Task<IEnumerable<TenderDto>> GetTendersOrderedByAmountAsync(bool asc = true)
        {
            if (_tendersCache == null)
                await GetAllTendersAsync();

            var query = _tendersCache.AsEnumerable();

            query = asc
                ? query.OrderBy(t => t.AmountEur)
                : query.OrderByDescending(t => t.AmountEur);

            return query;
        }

        public async Task<IEnumerable<TenderDto>> GetTendersByDateAsync(DateTime? startDate, DateTime? endDate)
        {
            if (_tendersCache == null)
                await GetAllTendersAsync();

            var query = _tendersCache.AsEnumerable();

            if (startDate.HasValue)
                query = query.Where(t => t.Date >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(t => t.Date <= endDate.Value);

            return query;
        }

        public async Task<IEnumerable<TenderDto>> GetTendersOrderedByDateAsync(bool asc = true)
        {
            if (_tendersCache == null)
                await GetAllTendersAsync();

            var query = _tendersCache.AsEnumerable();

            query = asc
                ? query.OrderBy(t => t.Date)
                : query.OrderByDescending(t => t.Date);

            return query;
        }

        public async Task<IEnumerable<TenderDto>> GetTendersBySupplierIdAsync(int? supplierId)
        {
            if (_tendersCache == null)
                await GetAllTendersAsync();

            var query = _tendersCache.AsEnumerable();

            if (supplierId != null)
                query = query.Where(t => t.Suppliers.Any(s => s.Id == supplierId));

            return query;
        }

        public async Task<IEnumerable<TenderDto>> GetTendersByTenderIdAsync(int? tenderId)
        {
            if (_tendersCache == null)
                await GetAllTendersAsync();

            var query = _tendersCache.AsEnumerable();

            if (tenderId != null)
                query = query.Where(t => t.Id == tenderId);

            return query;
        }
    }
}
