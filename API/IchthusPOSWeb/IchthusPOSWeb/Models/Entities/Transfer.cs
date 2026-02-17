namespace IchthusPOSWeb.Models.Entities
{
    public class Transfer
    {
        public int Id { get; set; }
        public string? ReleaseBy { get; set; }
        public string? ReceiveBy { get; set; }
        public string? Status { get; set; }
        public string? FromLocation { get; set; }
        public  string? ToLocation { get; set; }
        public DateTime? TransferredDate { get; set; }
        public List<TransferItem> Items { get; set; }
    }
}
