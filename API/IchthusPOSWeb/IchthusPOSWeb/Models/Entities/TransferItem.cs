namespace IchthusPOSWeb.Models.Entities
{
    public class TransferItem
    {
        public int? Id { get; set; }
        public int? PricelistId { get; set; }
        public int? ReceiverPricelistId { get; set; }
        public Pricelist? Pricelist { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public List<int> SerialNumberIds { get; set; }
    }
}
