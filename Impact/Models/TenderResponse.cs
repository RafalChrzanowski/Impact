using Impact.Models;

namespace Impact.Controllers
{
    public class TenderResponse
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int Total { get; set; }
        public List<Tender> Data { get; set; } = new();
    }
}
