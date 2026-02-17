namespace IchthusPOSWeb.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public byte[]? ProductImage { get; set; }
        public string? ProductName { get; set; }
        public required string ItemCode { get; set; }
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public int? CategoryTwoId { get; set; }
        public CategoryTwo CategoryTwo { get; set; }
        public int? CategoryThreeId { get; set; }
        public CategoryThree CategoryThree { get; set; }
        public int? CategoryFourId { get; set; }
        public CategoryFour CategoryFour { get; set; }
        public int? CategoryFiveId { get; set; }
        public CategoryFive CategoryFive { get; set; }
        public string? BarCode { get; set; }
        public string? Description { get; set; }
        public bool HasSerial { get; set; } // New property


    }
}
