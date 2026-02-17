namespace IchthusPOSWeb.Models.Entities
{
    public class CompletedTransferSerial
    {
        public int Id { get; set; }
        public int? SerialNumberId { get; set; }
        public SerialNumber SerialNumber { get; set; }
        public string? SerialName {  get; set; } 
        public string? Status { get; set; }
    }
}
