using Impact.Controllers;
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


            var response = await _httpClient.GetFromJsonAsync<TenderResponse>($"{BaseUrl}?page=1&page_size=100");
            var test = "";
            

            return _tendersCache;
        }
      
    }
}
