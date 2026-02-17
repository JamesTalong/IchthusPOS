using IchthusPOSWeb.Models.Entities;

namespace IchthusPOSWeb.Models
{
    public class AddCompletedTransferItemDto
    {
        public int? ReceiverPricelistId { get; set; }
        public int? PricelistId { get; set; }
        public int? Quantity { get; set; }
        public List<AddCompletedTransferSerialDto>? SerialNumbers { get; set; }
    }
}
