using IchthusPOSWeb.Models.Entities;

namespace IchthusPOSWeb.Models
{
    public class UpdateCompletedTransferItemDto
    {
        public int? ReceiverPricelistId { get; set; }
        public int? PricelistId { get; set; }
        public int? ItemCode { get; set; }  
        public int? Quantity { get; set; }
        public List<UpdateCompletedTransferSerialDto>? SerialNumbers { get; set; }
    }
}
