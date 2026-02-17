namespace IchthusPOSWeb.Models.Entities
{
    public class Batch
    {
        public int Id { get; set; }
        public DateTime? BatchDate { get; set; }

        public int? NumberOfItems { get; set; }
        public required int PricelistId { get; set; }
        public Pricelist Pricelist { get; set; }
        public bool? HasSerial { get; set; }
        public List<SerialNumber>? SerialNumbers { get; set; } = new List<SerialNumber>();

    }
}
