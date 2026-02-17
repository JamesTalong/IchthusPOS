namespace IchthusPOSWeb.Models.Entities
{
    public class ReceivedTransferItems
    {
        public int Id { get; set; }
        public int? CompletedTransferId { get; set; } // Foreign key
        public int? ReceiverPricelistId { get; set; }
        public int? ItemCode { get; set; }
        public string? ProductName { get; set; }
        public int? PricelistId { get; set; }
        public Pricelist Pricelist { get; set; }
        public int? Quantity { get; set; }
        public List<ReceivedTransferSerial>? SerialNumbers { get; set; } // Using List<T> and making it nullable
    }
}
