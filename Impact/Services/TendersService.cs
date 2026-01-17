using Impact.Mappers;
using Impact.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace Impact.Services
{
    public class TendersService : ITendersService
    {
        private const string CacheKey = "all_tenders";
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;
        private readonly IDistributedCache _cache;


        public TendersService(HttpClient httpClient, IDistributedCache cache, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _cache = cache;
            _baseUrl = configuration["BaseUrl"] ?? throw new ArgumentNullException("BaseUrl is missing in appsettings.json");
        }
        private async Task<List<TenderDto>> GetCachedTendersAsync()
        {
            _httpClient.Timeout = TimeSpan.FromMinutes(5);
            var cachedData = await _cache.GetStringAsync(CacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<List<TenderDto>>(cachedData)!;
            }

            var tenders = new List<TenderDto>();

            for (int page = 1; page <= 100; page++)
            {
                var response = await _httpClient.GetFromJsonAsync<TenderResponse>($"{_baseUrl}?page={page}");

                if (response?.Data == null || !response.Data.Any())
                    break;

                tenders.AddRange(response.Data.Select(TenderMapper.ToDto));
            }

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };

            await _cache.SetStringAsync(
                CacheKey,
                JsonSerializer.Serialize(tenders),
                options
            );

            return tenders;
        }

        public async Task<PagedResponse<TenderDto>> GetTendersAsync(TenderFilterDto filter)
        {
            var tenders = await GetCachedTendersAsync();
            var query = tenders.AsQueryable();

            if (filter.MinAmount.HasValue)
                query = query.Where(t => t.AmountEur >= filter.MinAmount.Value);

            if (filter.MaxAmount.HasValue)
                query = query.Where(t => t.AmountEur <= filter.MaxAmount.Value);

            if (filter.StartDate.HasValue)
                query = query.Where(t => t.Date >= filter.StartDate.Value);

            if (filter.EndDate.HasValue)
                query = query.Where(t => t.Date <= filter.EndDate.Value);

            if (filter.SupplierId.HasValue)
                query = query.Where(t => t.Suppliers.Any(s => s.Id == filter.SupplierId.Value));

            if (filter.TenderId.HasValue)
                query = query.Where(t => t.Id == filter.TenderId.Value);

            if (filter.OrderByAmountAsc.HasValue)
            {
                query = filter.OrderByAmountAsc.Value
                    ? query.OrderBy(t => t.AmountEur)
                    : query.OrderByDescending(t => t.AmountEur);
            }
            else if (filter.OrderByDateAsc.HasValue)
            {
                query = filter.OrderByDateAsc.Value
                    ? query.OrderBy(t => t.Date)
                    : query.OrderByDescending(t => t.Date);
            }

            var page = Math.Max(filter.Page, 1);
            var pageSize = Math.Clamp(filter.PageSize, 1, 100);

            var totalItems = query.Count();

            var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new PagedResponse<TenderDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = items
            };
        }
    }
}
