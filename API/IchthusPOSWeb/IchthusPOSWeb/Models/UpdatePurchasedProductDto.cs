using IchthusPOSWeb.Models.Entities;

namespace IchthusPOSWeb.Models
{
    public class UpdatePurchasedProductDto
    {
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
        public int? Subtotal { get; set; }
        public string? VatType { get; set; }
        public int? DiscountValue { get; set; }
        public List<SerialTemp>? SerialNumbers { get; set; } = new List<SerialTemp>();
    }
}
