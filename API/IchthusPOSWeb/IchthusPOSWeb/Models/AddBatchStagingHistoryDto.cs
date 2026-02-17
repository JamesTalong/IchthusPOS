using IchthusPOSWeb.Models.Entities;

namespace IchthusPOSWeb.Models
{
    public class AddBatchStagingHistoryDto
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime? BatchDate { get; set; }
        public int? NumberOfItems { get; set; }
        public required int PricelistId { get; set; }
        public bool? HasSerial { get; set; }
        public List<SerialStagingsHistory>? SerialStagings { get; set; } = new List<SerialStagingsHistory>();
    }
}
