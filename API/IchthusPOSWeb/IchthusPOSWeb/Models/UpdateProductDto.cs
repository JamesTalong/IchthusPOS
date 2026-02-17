namespace IchthusPOSWeb.Models
{
    public class UpdateProductDto
    {
        public byte[]? ProductImage { get; set; }
        public string? ProductName { get; set; }
        public string? ItemCode { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public int? CategoryTwoId { get; set; }
        public int? CategoryThreeId { get; set; }
        public int? CategoryFourId { get; set; }
        public int? CategoryFiveId { get; set; }
        public string? BarCode { get; set; }
        public string? Description { get; set; }
        public bool? HasSerial { get; set; } 
    }
}
