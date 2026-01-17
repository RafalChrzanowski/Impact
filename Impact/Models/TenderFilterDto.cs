namespace Impact.Models
{
    public class TenderFilterDto
    {
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? SupplierId { get; set; }
        public int? TenderId { get; set; }
        public bool? OrderByAmountAsc { get; set; }
        public bool? OrderByDateAsc { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
