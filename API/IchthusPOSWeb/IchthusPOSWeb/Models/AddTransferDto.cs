using IchthusPOSWeb.Models.Entities;

namespace IchthusPOSWeb.Models
{
    public class AddTransferDto
    {
        public int Id { get; set; }
        public string? ReleaseBy { get; set; }
        public string? ReceiveBy { get; set; }
        public string? Status { get; set; }
        public string? FromLocation { get; set; }
        public string? ToLocation { get; set; }
        public DateTime? TransferredDate { get; set; }
        public List<TransferItem> Items { get; set; }
    }
}
