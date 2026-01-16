using System.Text.Json.Serialization;

namespace Impact.Models
{
    public class Awarded
    {
        public string Date { get; set; }

        public string SuppliersId { get; set; }

        public string SuppliersName { get; set; }

        public List<Supplier> Suppliers { get; set; } = new();
    }

}
