namespace IchthusPOSWeb.Models.Entities
{
    public class SerialMain
    {
        public int Id { get; set; } 
        public required int PricelistId { get; set; } 
        public List<int> SerialNumbers { get; set; } 
    }
}

