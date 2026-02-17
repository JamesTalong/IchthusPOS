using IchthusPOSWeb.Models.Entities;

namespace IchthusPOSWeb.Models
{
    public class AddReceivedTransferItemDto
    {
        public int? ReceiverPricelistId { get; set; }
        public int? PricelistId { get; set; }
        public int? ItemCode { get; set; }
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }
        public List<AddReceivedTransferSerialDto>? SerialNumbers { get; set; }
    }
}
