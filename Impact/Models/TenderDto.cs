namespace Impact.Models
{
    public class TenderDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal AmountEur { get; set; }
        public List<Supplier> Suppliers { get; set; }
    }
}
