using Impact.Models;
using System.Globalization;

namespace Impact.Mappers
{
    public class TenderMapper
    {
        public static TenderDto ToDto(Tender tender)
        {
            var suppliers = tender.Awarded.SelectMany(a => a.Suppliers).ToList();

            return new TenderDto
            {
                Id = tender.Id,
                Date = ParseDate(tender.Date),
                Title = tender.Title,
                Description = tender.Description,
                AmountEur = tender.AmountEur,
                Suppliers = suppliers
            };
        }

        private static DateTime ParseDate(string date)
        {
            return DateTime.Parse(
                date,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal
            );
        }
    }
}