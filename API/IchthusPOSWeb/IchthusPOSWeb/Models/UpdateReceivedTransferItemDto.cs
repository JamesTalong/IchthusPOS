using IchthusPOSWeb.Models.Entities;

namespace IchthusPOSWeb.Models
{
    public class UpdateReceivedTransferItemDto
    {
        public int? ReceiverPricelistId { get; set; }
        public int? PricelistId { get; set; }
        public int? ItemCode { get; set; }
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }
        public List<UpdateCompletedTransferSerialDto>? SerialNumbers { get; set; }
    }
}
