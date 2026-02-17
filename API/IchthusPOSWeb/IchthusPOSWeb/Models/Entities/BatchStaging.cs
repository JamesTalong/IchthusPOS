namespace IchthusPOSWeb.Models.Entities
{
    public class BatchStaging
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime? BatchDate { get; set; }
        public int? NumberOfItems { get; set; }
        public required int PricelistId { get; set; }
        public Pricelist Pricelist { get; set; }
        public bool? HasSerial { get; set; }
        public List<SerialStaging>? SerialStagings { get; set; } = new List<SerialStaging>();

    }
}
