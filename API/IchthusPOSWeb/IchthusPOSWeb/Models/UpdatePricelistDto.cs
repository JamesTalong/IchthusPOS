namespace IchthusPOSWeb.Models
{
    public class UpdatePricelistDto
    {
        public int? ProductId { get; set; }
        public int? LocationId { get; set; }
        public int? ColorId { get; set; }
        public int? VatEx { get; set; }
        public int? VatInc { get; set; }
        public int? Reseller { get; set; }
        public int? ZeroRated { get; set; }
    }
}
