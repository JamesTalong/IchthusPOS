namespace IchthusPOSWeb.Models.Entities
{
    public class CompletedTransfer
    {
        public int Id { get; set; }
        public int? TransferId { get; set; } 
        public string? FromLocation { get; set; } // string is already nullable
        public string? ToLocation { get; set; }
        public string? Status { get; set; }
        public string? ReleaseBy { get; set; }
        public string? ReceiveBy { get; set; }
        public DateTime? TransferredDate { get; set; }
        public DateTime? RecievedDate { get; set; }
        public List<CompletedTransferItems>? Items { get; set; } // Using List<T> and making it nullable
    }
}
