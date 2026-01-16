using System.Text.Json.Serialization;

namespace Impact.Models
{
    public class Tender
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [JsonPropertyName("awarded_value_eur")]
        public decimal AmountEur { get; set; }
        public List<Awarded> Awarded { get; set; } = new();
    }
}
