using IchthusPOSWeb.Models.Entities;

namespace IchthusPOSWeb.Models
{
    public class UpdateCompletedTransferDto
    {
        public int? TransferId { get; set; }
        public string? FromLocation { get; set; }
        public string? ToLocation { get; set; }
        public string? Status { get; set; }
        public string? ReleaseBy { get; set; }
        public string? ReceiveBy { get; set; }
        public DateTime? TransferredDate { get; set; }
        public DateTime? RecievedDate { get; set; }
        public List<UpdateCompletedTransferItemDto>? Items { get; set; }
    }
}
