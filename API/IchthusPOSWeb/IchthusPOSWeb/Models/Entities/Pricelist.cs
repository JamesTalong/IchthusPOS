namespace IchthusPOSWeb.Models.Entities
{
    public class Pricelist
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public int? LocationId { get; set; }
        public Location Location { get; set; }
        public int? ColorId { get; set; }
        public Color Color { get; set; }
        public int? VatEx { get; set; }
        public int? VatInc { get; set; }
        public int? Reseller { get; set; }
        public int? ZeroRated { get; set; }
        public List<Batch>? Batches { get; set; } = new List<Batch>();

    }

}

