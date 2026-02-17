using IchthusPOSWeb.Models.Entities;

namespace IchthusPOSWeb.Models.Entities
{
    public class PurchasedProduct
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
        public int? Subtotal { get; set; }
        public string? VatType { get; set; }
        public int? DiscountValue { get; set; }
        public List<SerialMain>? SerialNumbers { get; set; } = new List<SerialMain>();

    }
}
