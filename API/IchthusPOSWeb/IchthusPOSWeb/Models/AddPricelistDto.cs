namespace IchthusPOSWeb.Models
{
    public class AddPricelistDto
    {
        public required int ProductId { get; set; }
        public required int LocationId { get; set; }
        public required int ColorId { get; set; }
        public int? VatEx { get; set; }
        public int? VatInc { get; set; }
        public int? Reseller { get; set; }
        public int? ZeroRated { get; set; }
    }
}
