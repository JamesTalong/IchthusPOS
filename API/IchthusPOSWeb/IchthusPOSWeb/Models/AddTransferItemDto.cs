using IchthusPOSWeb.Models.Entities;

namespace IchthusPOSWeb.Models
{
    public class AddTransferItemDto
    {
        public string? FromLocation { get; set; }
        public string? ToLocation { get; set; }

        public DateTime? TransferredDate { get; set; }
        public List<TransferItem> Items { get; set; }
    }
}
