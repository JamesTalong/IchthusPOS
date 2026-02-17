using IchthusPOSWeb.Models.Entities;

namespace IchthusPOSWeb.Models
{
    public class AddBatchDto
    {
        public DateTime? BatchDate { get; set; }
        public int? NumberOfItems { get; set; }
        public required int PricelistId { get; set; }
        public bool? HasSerial { get; set; }
        public List<SerialNumber>? SerialNumbers { get; set; } = new List<SerialNumber>();
    }
}
